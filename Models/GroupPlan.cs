namespace TripMate_TeodorLazar.Models
{
    public class GroupPlan
    {
        public string GroupId { get; set; } = "";
        public int Version { get; set; } = 0;
        public List<string> Stops { get; set; } = new();
        public string UpdatedByEmail { get; set; } = "";
        public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
