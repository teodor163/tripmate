using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Services
{
    public static class ShareService
    {
        private static readonly object _lock = new();
        private static readonly Dictionary<string, ShareLink> _links = new(); // token -> link

        public static ShareLink Create(string groupId, TimeSpan? ttl = null)
        {
            lock (_lock)
            {
                var l = new ShareLink
                {
                    GroupId = groupId,
                    IsActive = true,
                    ExpiresAtUtc = ttl.HasValue ? DateTime.UtcNow.Add(ttl.Value) : null
                };
                _links[l.Token] = l;
                return l;
            }
        }

        public static bool Disable(string token)
        {
            lock (_lock)
            {
                if (!_links.TryGetValue(token, out var l)) return false;
                l.IsActive = false;
                return true;
            }
        }

        public static (bool ok, string error, ShareLink? link) Get(string token)
        {
            lock (_lock)
            {
                if (!_links.TryGetValue(token, out var l))
                    return (false, "Link not found", null);

                if (!l.IsActive)
                    return (false, "Link disabled", l);

                if (l.ExpiresAtUtc.HasValue && DateTime.UtcNow > l.ExpiresAtUtc.Value)
                    return (false, "Link expired", l);

                return (true, "", l);
            }
        }

        // for tests
        public static void ClearForTests()
        {
            lock (_lock) { _links.Clear(); }
        }
    }
}
