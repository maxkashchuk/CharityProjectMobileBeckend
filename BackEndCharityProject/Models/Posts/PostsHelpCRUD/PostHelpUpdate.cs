using BackEndCharityProject.Models.Posts.Additional;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndCharityProject.Models.Posts.PostsHelpCRUD
{
    [NotMapped]
    public class PostHelpUpdate
    {
        public int? Money { get; set; }
        public string Header { get; set; }
        public ICollection<string> Images { get; set; }
        public string Description { get; set; }
        public double? Lattitude { get; set; }
        public double? Longtitude { get; set; }
        public ICollection<string> Tags { get; set; }
    }
}
