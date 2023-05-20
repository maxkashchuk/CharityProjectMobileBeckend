using BackEndCharityProject.Models;
using BackEndCharityProject.Models.Posts;
using BackEndCharityProject.Models.Posts.Additional;
using Microsoft.EntityFrameworkCore;

namespace BackEndCharityProject
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PostHelp> PostsHelp { get; set; }
        public DbSet<PostHelp> PostsVolunteer { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Image> Images { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<User>().Property(x => x.Name).IsRequired();
            builder.Entity<User>().Property(x => x.Surname).IsRequired();
            builder.Entity<User>().Property(x => x.Email).IsRequired();
            builder.Entity<User>().Property(x => x.Password).IsRequired();
            builder.Entity<User>().Property(x => x.PasswordHash).IsRequired();
            builder.Entity<User>().Property(x => x.Date).IsRequired();

            builder.Entity<PostHelp>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<PostHelp>().Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<PostHelp>().Property(x => x.Description).IsRequired();
            builder.Entity<PostHelp>().Property(x => x.Header).IsRequired();

            builder.Entity<PostVolunteer>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<PostVolunteer>().Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<PostVolunteer>().Property(x => x.Description).IsRequired();
            builder.Entity<PostVolunteer>().Property(x => x.Header).IsRequired();
            builder.Entity<PostVolunteer>().Property(x => x.Goal).IsRequired();
            builder.Entity<PostVolunteer>().Property(x => x.Donated).IsRequired();

            builder.Entity<Tag>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<Tag>().Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Entity<Image>().HasIndex(x => x.Id).IsUnique();
            builder.Entity<Image>().Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
