using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndCharityProject.Models.Posts.Additional
{
    [NotMapped]
    public class PostSearch
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }
        public double RatingStart { get; set; }
        public double RatingEnd { get; set; }
    }
}
