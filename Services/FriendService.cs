using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Services
{
    public static class FriendService
    {
        private static readonly object _lock = new();
        private static readonly Dictionary<string, FriendInvite> _invites = new(); // inviteId -> invite

        public static (bool ok, string error, FriendInvite? invite) SendInvite(
            string fromEmail,
            string toEmail,
            string groupId)
        {
            if (string.IsNullOrWhiteSpace(toEmail))
                return (false, "Missing email", null);

            // validate user exists (uses your existing UserStore)
            var to = UserStore.FindByEmail(toEmail);
            if (to == null)
                return (false, "User does not exist", null);

            if (!GroupService.IsMember(groupId, fromEmail))
                return (false, "You are not a member of this group", null);

            lock (_lock)
            {
                // avoid duplicate pending invites
                var exists = _invites.Values.Any(i =>
                    i.Status == InviteStatus.Pending &&
                    i.FromEmail.Equals(fromEmail, StringComparison.OrdinalIgnoreCase) &&
                    i.ToEmail.Equals(toEmail, StringComparison.OrdinalIgnoreCase) &&
                    i.GroupId == groupId);

                if (exists)
                    return (false, "Invite already sent", null);

                var inv = new FriendInvite
                {
                    FromEmail = fromEmail,
                    ToEmail = toEmail.Trim(),
                    GroupId = groupId,
                    Status = InviteStatus.Pending
                };

                _invites[inv.Id] = inv;
                return (true, "", inv);
            }
        }

        public static List<FriendInvite> GetIncoming(string userEmail)
        {
            lock (_lock)
            {
                return _invites.Values
                    .Where(i => i.ToEmail.Equals(userEmail, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(i => i.CreatedAtUtc)
                    .ToList();
            }
        }

        public static List<FriendInvite> GetOutgoing(string userEmail)
        {
            lock (_lock)
            {
                return _invites.Values
                    .Where(i => i.FromEmail.Equals(userEmail, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(i => i.CreatedAtUtc)
                    .ToList();
            }
        }

        public static (bool ok, string error) Respond(string inviteId, string userEmail, bool accept)
        {
            lock (_lock)
            {
                if (!_invites.TryGetValue(inviteId, out var inv))
                    return (false, "Invite not found");

                if (!inv.ToEmail.Equals(userEmail, StringComparison.OrdinalIgnoreCase))
                    return (false, "Not your invite");

                if (inv.Status != InviteStatus.Pending)
                    return (false, "Invite already resolved");

                if (accept)
                {
                    inv.Status = InviteStatus.Accepted;
                    GroupService.AddMember(inv.GroupId, inv.ToEmail);
                }
                else
                {
                    inv.Status = InviteStatus.Rejected;
                }

                return (true, "");
            }
        }

        // for tests
        public static void ClearForTests()
        {
            lock (_lock) { _invites.Clear(); }
        }
    }
}
