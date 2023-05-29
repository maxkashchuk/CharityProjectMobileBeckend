using BackEndCharityProject.Models;
using BackEndCharityProject.Models.AuthModels;
using BackEndCharityProject.Models.Posts;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BackEndCharityProject.Services
{
    public interface IUserService
    {
        public Task<bool> UserRegister(UserRegister user);
        public int? UserLogin(UserLogin user);
        public Task<User> GetCertainUser(int id);
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<bool> UpdateUser(int id, UserUpdate user);
        public Task<bool> SetRating(int id_origin, int id_vote, double rating);
    }
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        public UserService(ApplicationContext _context)
        {
            this._context = _context;
        }
        private string Hash(string str)
        {
            HashAlgorithm hash = SHA256.Create();
            byte[] arr = hash.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in arr)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
        public async Task<bool> UserRegister(UserRegister user)
        {
            User u = new User()
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Password = user.Password,
                PasswordHash = Hash(user.Password),
                Date = user.Date,
                Avatar = user.Avatar,
                Gender = user.Gender,
                Rating = 0
            };
            await _context.Users.AddAsync(u);
            await _context.SaveChangesAsync();
            return true;
        }

        public int? UserLogin(UserLogin user)
        {
            User u = _context.Users.Where(u => u.Email == user.Email && u.PasswordHash == Hash(user.Password)).SingleOrDefault();
            if (u != null)
            {
                return _context.Users.Where(us => us.PasswordHash == u.PasswordHash).SingleOrDefault().Id;
            }
            return null;
        }

        public async Task<User> GetCertainUser(int id)
        {
            return await _context.Users.Where(u => u.Id.ToString() == id.ToString()).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> UpdateUser(int id, UserUpdate user)
        {
            StringBuilder pasHash = new StringBuilder();
            pasHash.Append(Hash(user.Password));
            User u = await _context.Users.SingleOrDefaultAsync(us => us.Id == id);
            u.Name = (user.Name != null) ? user.Name : u.Name;
            u.Surname = (user.Surname != null) ? user.Surname : u.Surname;
            u.Email = (user.Email != null) ? user.Email : u.Email;
            u.Password = (user.Password != null) ? user.Password : u.Password;
            u.PasswordHash = (user.Password != null) ? pasHash.ToString() : u.PasswordHash;
            u.Date = (user.Date != null) ? user.Date : u.Date;
            u.Avatar = (user.Avatar != null) ? user.Avatar : u.Avatar;
            u.Gender = (user.Gender != null) ? user.Gender : u.Gender;
            _context.Users.Update(u);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetRating(int id_origin, int id_vote, double rating)
        {
            User user_origin = await _context.Users.Where(el => el.Id == id_origin).FirstOrDefaultAsync();
            User user_vote = await _context.Users.Where(el => el.Id == id_vote).FirstOrDefaultAsync();
            user_origin.Ratings = await _context.Ratings.Where(el => el.UserOriginId == user_origin.Id).ToListAsync();
            foreach (Rating r in user_origin.Ratings)
            {
                if (r.UserVoteId == id_vote)
                {
                    r.Value = rating;
                    user_origin.Rating = user_origin.Ratings.Sum(el => el.Value) / user_origin.Ratings.Count;
                    _context.Users.Update(user_origin);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            user_origin.Ratings.Add(new Rating()
            {
                UserOrigin = user_origin,
                UserVote = user_vote,
                Value = rating
            });
            user_origin.Rating = user_origin.Ratings.Sum(el => el.Value) / user_origin.Ratings.Count;
            _context.Users.Update(user_origin);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
