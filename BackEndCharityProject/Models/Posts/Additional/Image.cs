namespace BackEndCharityProject.Models.Posts.Additional
{
    public class Image
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public PostHelp PostHelps { get; set; }
        public PostVolunteer PostVolunteers { get; set; }
    }
}
