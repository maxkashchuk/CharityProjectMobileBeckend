namespace BackEndCharityProject.Models.Posts
{
    public class Rating
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public int UserOriginId { get; set; }
        public User UserOrigin { get; set; }
        public int UserVoteId { get; set; }
        public User UserVote { get; set; }
    }
}
