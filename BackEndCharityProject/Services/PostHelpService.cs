using BackEndCharityProject.Models;
using BackEndCharityProject.Models.Posts;
using BackEndCharityProject.Models.Posts.Additional;
using BackEndCharityProject.Models.Posts.PostsHelpCRUD;
using Microsoft.EntityFrameworkCore;

namespace BackEndCharityProject.Services
{
    public interface IPostHelpService
    {
        public Task<bool> PostCreate(PostHelpAdd post);
        public Task<PostHelpRead> PostRead(int id);
        public Task<bool> PostUpdate(int id, PostHelpUpdate post);
        public Task<bool> PostDelete(int id);
        public IEnumerable<PostHelp> GetAllPosts();
        public Task<IEnumerable<PostHelpRead>> UserPostsGet(int id);
    }
    public class PostHelpService : IPostHelpService
    {
        private readonly ApplicationContext _context;
        public PostHelpService(ApplicationContext _context)
        {
            this._context = _context;
        }

        public IEnumerable<PostHelp> GetAllPosts()
        {
            return _context.PostsHelp.ToList();
        }

        public async Task<bool> PostCreate(PostHelpAdd post)
        {
            List<Image> images = new List<Image>();
            List<Tag> tags = new List<Tag>();
            User user = await _context.Users.Where(el => el.Id == post.UserID).SingleOrDefaultAsync();
            for (int i = 0; i < post.Images.ToList().Count; i++)
            {
                images.Add(new Image() { Value = post.Images.ToList()[i] });
            }
            for(int i = 0; i < post.Tags.ToList().Count; i++)
            {
                tags.Add(new Tag() { Title = post.Tags.ToList()[i] });
            }
            PostHelp p = new PostHelp()
            {
                Money = post.Money,
                Header = post.Header,
                Images = images,
                Description = post.Description,
                Lattitude = post.Lattitude,
                Longtitude = post.Longtitude,
                Tags = tags,
                User = user
            };
            await _context.PostsHelp.AddAsync(p);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PostDelete(int id)
        {
            PostHelp p = await _context.PostsHelp.Where(el => el.Id == id).FirstOrDefaultAsync();
            _context.Tags.RemoveRange(await _context.Tags.Where(el => el.PostHelps == p).ToListAsync());
            _context.Images.RemoveRange(await _context.Images.Where(el => el.PostHelps == p).ToListAsync());
            _context.PostsHelp.Remove(p);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PostHelpRead> PostRead(int id)
        {
            PostHelp p = await _context.PostsHelp.Where(el => el.Id == id).FirstOrDefaultAsync();
            List<Image> i = await _context.Images.Where(el => el.PostHelps.Id == p.Id).ToListAsync();
            List<Tag> t = await _context.Tags.Where(el => el.PostHelps.Id == p.Id).ToListAsync();
            User u = await _context.Users.Where(el => el.Id == p.UserId).SingleOrDefaultAsync();
            PostHelpRead pg = new PostHelpRead()
            {
                Id = p.Id,
                Money = p.Money,
                Description = p.Description,
                Header = p.Header,
                Lattitude = p.Lattitude,
                Longtitude = p.Longtitude,
                Images = i.Select(el => el.Value).ToList(),
                Tags = t.Select(el => el.Title).ToList(),
                User = new UserPostHelpGet()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    Email = u.Email,
                    Password = u.Password,
                    PasswordHash = u.PasswordHash,
                    Date = u.Date,
                    Avatar = u.Avatar,
                    Gender = u.Gender,
                    Rating = u.Rating
                }
            }; 
            return pg;
        }

        public async Task<bool> PostUpdate(int id, PostHelpUpdate post)
        {
            PostHelp p = await _context.PostsHelp.Where(el => el.Id == id).FirstOrDefaultAsync();
            p.Header = (post.Header != null) ? post.Header : p.Header;
            if(post.Images != null)
            {
                _context.Images.RemoveRange(await _context.Images.Where(el => el.PostHelps == p).ToListAsync());
                await _context.SaveChangesAsync();
                List<Image> images = new List<Image>();
                for (int i = 0; i < post.Images.Count; i++)
                {
                    images.Add(new Image() { Value = post.Images.ToList()[i] });
                }
                p.Images = images;
            }
            p.Description = (post.Description != null) ? post.Description : p.Description;
            p.Lattitude = (post.Lattitude != null) ? post.Lattitude : p.Lattitude;
            p.Longtitude = (post.Longtitude != null) ? post.Longtitude : p.Longtitude;
            if(post.Tags != null)
            {
                _context.Tags.RemoveRange(await _context.Tags.Where(el => el.PostHelps == p).ToListAsync());
                await _context.SaveChangesAsync();
                List<Tag> tags = new List<Tag>();
                for (int i = 0; i < post.Tags.Count; i++)
                {
                    tags.Add(new Tag() { Title = post.Tags.ToList()[i] });
                }
                p.Tags = tags;
            }
            _context.Update(p);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PostHelpRead>> UserPostsGet(int id)
        {
            List<PostHelpRead> posts = new List<PostHelpRead>();

            List<PostHelp> posts_help = await _context.PostsHelp.Where(el => el.UserId == id).ToListAsync();

            foreach(PostHelp p in posts_help)
            {
                posts.Add(await PostRead(p.Id));
            }
            
            return posts;
        }
    }
}
