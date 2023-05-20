using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndCharityProject.Models
{
    [NotMapped]
    public class UserUpdate
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public DateTime Date { get; set; }
        public string Avatar { get; set; }
        public bool Gender { get; set; }
    }
}
