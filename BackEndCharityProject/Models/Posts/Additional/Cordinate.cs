using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndCharityProject.Models.Posts.Additional
{
    [NotMapped]
    public class Cordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
