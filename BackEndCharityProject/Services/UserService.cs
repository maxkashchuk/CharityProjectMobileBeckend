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
        public Task<bool> SetRating(int id, double rating);
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
                Gender = user.Gender
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

        public async Task<bool> SetRating(int id, double rating)
        {
            User user = await _context.Users.Where(el => el.Id == id).FirstOrDefaultAsync();
            user.Rating = rating;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
