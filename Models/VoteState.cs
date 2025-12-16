namespace TripMate_TeodorLazar.Models
{

    public class VoteState
    {
        public string GroupId { get; set; } = "";
        public Dictionary<string, string> UserVotes { get; set; } = new();
    }
}
