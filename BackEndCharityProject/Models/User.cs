using BackEndCharityProject.Models.Posts;

namespace BackEndCharityProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public DateTime Date { get; set; }
        public ICollection<PostHelp> Posts { get; set; }
        public string Avatar { get; set; }
        public bool Gender { get; set; }
        public double? Rating { get; set; }

    }
}
