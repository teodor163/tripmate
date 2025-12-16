namespace TripMate_TeodorLazar.Models
{
    public enum InviteStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class FriendInvite
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string FromEmail { get; set; } = "";
        public string ToEmail { get; set; } = "";
        public string GroupId { get; set; } = "";
        public InviteStatus Status { get; set; } = InviteStatus.Pending;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
