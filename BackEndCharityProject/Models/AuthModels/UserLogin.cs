using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndCharityProject.Models.AuthModels
{
    [NotMapped]
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
