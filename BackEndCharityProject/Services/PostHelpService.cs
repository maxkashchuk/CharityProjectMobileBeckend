using BackEndCharityProject.Models;
using BackEndCharityProject.Models.Posts;
using BackEndCharityProject.Models.Posts.Additional;
using BackEndCharityProject.Models.Posts.PostsHelpCRUD;
using Geolocation;
using Microsoft.EntityFrameworkCore;

namespace BackEndCharityProject.Services
{
    public interface IPostHelpService
    {
        public Task<bool> PostCreate(PostHelpAdd post);
        public Task<PostHelpRead> PostRead(int id);
        public Task<bool> PostUpdate(int id, PostHelpUpdate post);
        public Task<bool> PostDelete(int id);
        public Task<IEnumerable<PostHelpRead>> GetAllPosts();
        public Task<IEnumerable<PostHelpRead>> UserPostsGet(int id);
        public Task<List<PostHelpRead>> PostCordinatesSearch(PostSearch search);
        public Task<List<PostHelpRead>> PostRatingSearch(PostSearch search);
        public Task<List<PostHelpRead>> PostDescriptionSearch(PostSearch search);
        public Task<List<PostHelpRead>> PostHeaderSearch(PostSearch search);
        public Task<List<PostHelpRead>> PostUserNameSearch(PostSearch search);
        public Task<List<PostHelpRead>> PostUserSurnameSearch(PostSearch search);
    }
    public class PostHelpService : IPostHelpService
    {
        private readonly ApplicationContext _context;
        public PostHelpService(ApplicationContext _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<PostHelpRead>> GetAllPosts()
        {
            List<PostHelpRead> posts = new List<PostHelpRead>();

            List<PostHelp> posts_help = await _context.PostsHelp.ToListAsync();

            foreach (PostHelp p in posts_help)
            {
                posts.Add(await PostRead(p.Id));
            }

            return posts;
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
            p.Money = (post.Money != null) ? post.Money : p.Money;
            p.Lattitude = post.Lattitude;
            p.Longtitude = post.Longtitude;
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

        public async Task<List<PostHelpRead>> PostUserNameSearch(PostSearch search)
        {
            List<PostHelpRead> posts = new List<PostHelpRead>();

            List<PostHelp> postsHelp = new List<PostHelp>();

            List<User> users = await _context.Users.Where(el => el.Name.Contains(search.Name)).ToListAsync();

            foreach (User user in users) 
            {
                postsHelp.AddRange(await _context.PostsHelp.Where(el => el.UserId == user.Id).ToListAsync());
            }

            foreach(PostHelp p in postsHelp)
            {
                posts.Add(await PostRead(p.Id));
            }

            return posts;
        }

        public async Task<List<PostHelpRead>> PostUserSurnameSearch(PostSearch search)
        {
            List<PostHelpRead> posts = new List<PostHelpRead>();

            List<PostHelp> postsHelp = new List<PostHelp>();

            List<User> users = await _context.Users.Where(el => el.Surname.Contains(search.Surname)).ToListAsync();

            foreach (User user in users)
            {
                postsHelp.AddRange(await _context.PostsHelp.Where(el => el.UserId == user.Id).ToListAsync());
            }

            foreach (PostHelp p in postsHelp)
            {
                posts.Add(await PostRead(p.Id));
            }

            return posts;
        }

        public async Task<List<PostHelpRead>> PostCordinatesSearch(PostSearch search)
        {
            List<PostHelp> postsTemporary = await _context.PostsHelp.ToListAsync();
            List<PostHelpRead> posts = new List<PostHelpRead>();
            foreach(PostHelp post in postsTemporary) 
            {
                if(GeoCalculator.GetDistance(search.Latitude, search.Longitude, (double)post.Lattitude, (double)post.Longtitude, 1, DistanceUnit.Kilometers) < search.Distance)
                {
                    posts.Add(await PostRead(post.Id));
                }
                
            }
            return posts;
        }

        public async Task<List<PostHelpRead>> PostRatingSearch(PostSearch search)
        {
            List<PostHelp> postsTemporary = new List<PostHelp>();
            List<PostHelpRead> posts = new List<PostHelpRead>();
            List<User> users = new List<User>();
            List<Rating> ratings = new List<Rating>();
            users = await _context.Users.Where(el => el.Rating >= search.RatingStart && el.Rating <= search.RatingEnd).ToListAsync();
            foreach (User u in users)
            {
                postsTemporary = await _context.PostsHelp.Where(el => el.UserId == u.Id).ToListAsync();
            }
            foreach (PostHelp post in postsTemporary)
            {
                posts.Add(await PostRead(post.Id));
            }
            return posts;
        }

        public async Task<List<PostHelpRead>> PostDescriptionSearch(PostSearch search)
        {
            List<PostHelp> posts = new List<PostHelp>();
            List<PostHelpRead> pg = new List<PostHelpRead>();
            posts.AddRange(await _context.PostsHelp.Where(el => el.Description.Contains(search.Description)).ToListAsync());
            foreach (PostHelp post in posts)
            {
                pg.Add(await PostRead(post.Id));
            }
            return pg;
        }

        public async Task<List<PostHelpRead>> PostHeaderSearch(PostSearch search)
        {
            List<PostHelp> posts = new List<PostHelp>();
            List<PostHelpRead> pg = new List<PostHelpRead>();
            posts.AddRange(await _context.PostsHelp.Where(el => el.Header.Contains(search.Header)).ToListAsync());
            foreach(PostHelp post in posts)
            {
                pg.Add(await PostRead(post.Id));
            }
            return pg;
        }
    }
}
