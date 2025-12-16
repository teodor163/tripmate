namespace TripMate_TeodorLazar.Models
{
    public class ShareLink
    {
        public string Token { get; set; } = Guid.NewGuid().ToString("N");
        public string GroupId { get; set; } = "";
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAtUtc { get; set; } // null = no expiry
    }
}
