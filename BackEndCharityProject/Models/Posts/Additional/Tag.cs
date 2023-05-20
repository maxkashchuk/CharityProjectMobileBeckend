namespace BackEndCharityProject.Models.Posts.Additional
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public PostHelp PostHelps { get; set; }
        public PostVolunteer PostVolunteers { get; set; }
    }
}
