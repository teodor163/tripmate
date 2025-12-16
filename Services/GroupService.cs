using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Services
{
    public static class GroupService
    {
        private static readonly object _lock = new();
        private static readonly Dictionary<string, Group> _groups = new(); // groupId -> group
        private static readonly Dictionary<string, string> _defaultGroupByUser = new(); // email -> groupId

        public static Group EnsureDefaultGroup(string userEmail)
        {
            lock (_lock)
            {
                if (_defaultGroupByUser.TryGetValue(userEmail, out var gid) && _groups.ContainsKey(gid))
                    return _groups[gid];

                var g = new Group
                {
                    Name = "Default Group",
                    Members = new List<string> { userEmail }
                };

                _groups[g.Id] = g;
                _defaultGroupByUser[userEmail] = g.Id;
                return g;
            }
        }

        public static List<Group> GetGroupsFor(string userEmail)
        {
            lock (_lock)
            {
                return _groups.Values
                    .Where(g => g.Members.Any(m => m.Equals(userEmail, StringComparison.OrdinalIgnoreCase)))
                    .OrderBy(g => g.CreatedAtUtc)
                    .ToList();
            }
        }

        public static Group? GetGroup(string groupId)
        {
            lock (_lock)
            {
                return _groups.TryGetValue(groupId, out var g) ? g : null;
            }
        }

        public static bool IsMember(string groupId, string userEmail)
        {
            var g = GetGroup(groupId);
            if (g == null) return false;
            return g.Members.Any(m => m.Equals(userEmail, StringComparison.OrdinalIgnoreCase));
        }

        public static void AddMember(string groupId, string userEmail)
        {
            lock (_lock)
            {
                if (!_groups.TryGetValue(groupId, out var g)) return;
                if (!g.Members.Any(m => m.Equals(userEmail, StringComparison.OrdinalIgnoreCase)))
                    g.Members.Add(userEmail);
            }
        }

        // for tests
        public static void ClearForTests()
        {
            lock (_lock)
            {
                _groups.Clear();
                _defaultGroupByUser.Clear();
            }
        }
    }
}
