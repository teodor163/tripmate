namespace TripMate_TeodorLazar.Models
{
    public class Group
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Name { get; set; } = "My Group";
        public List<string> Members { get; set; } = new();
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
