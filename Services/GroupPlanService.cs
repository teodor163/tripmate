using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Services
{
    public static class GroupPlanService
    {
        private static readonly object _lock = new();
        private static readonly Dictionary<string, GroupPlan> _plans = new(); // groupId -> plan

        public static GroupPlan GetOrCreate(string groupId)
        {
            lock (_lock)
            {
                if (_plans.TryGetValue(groupId, out var p))
                    return p;

                p = new GroupPlan { GroupId = groupId, Version = 0 };
                _plans[groupId] = p;
                return p;
            }
        }

        // "Real time" simulation: optimistic concurrency by Version
        public static (bool ok, string error, GroupPlan plan) AddStop(string groupId, int clientVersion, string userEmail, string stop)
        {
            lock (_lock)
            {
                var p = GetOrCreate(groupId);

                if (clientVersion != p.Version)
                    return (false, "CONFLICT: plan updated by someone else. Refresh page.", p);

                if (!string.IsNullOrWhiteSpace(stop))
                {
                    p.Stops.Add(stop.Trim());
                    Touch(p, userEmail);
                }

                return (true, "", p);
            }
        }

        public static (bool ok, string error, GroupPlan plan) RemoveStop(string groupId, int clientVersion, string userEmail, int index)
        {
            lock (_lock)
            {
                var p = GetOrCreate(groupId);
                if (clientVersion != p.Version)
                    return (false, "CONFLICT: plan updated by someone else. Refresh page.", p);

                if (index >= 0 && index < p.Stops.Count)
                {
                    p.Stops.RemoveAt(index);
                    Touch(p, userEmail);
                }

                return (true, "", p);
            }
        }

        public static (bool ok, string error, GroupPlan plan) MoveUp(string groupId, int clientVersion, string userEmail, int index)
        {
            lock (_lock)
            {
                var p = GetOrCreate(groupId);
                if (clientVersion != p.Version)
                    return (false, "CONFLICT: plan updated by someone else. Refresh page.", p);

                if (index > 0 && index < p.Stops.Count)
                {
                    (p.Stops[index - 1], p.Stops[index]) = (p.Stops[index], p.Stops[index - 1]);
                    Touch(p, userEmail);
                }

                return (true, "", p);
            }
        }

        public static (bool ok, string error, GroupPlan plan) MoveDown(string groupId, int clientVersion, string userEmail, int index)
        {
            lock (_lock)
            {
                var p = GetOrCreate(groupId);
                if (clientVersion != p.Version)
                    return (false, "CONFLICT: plan updated by someone else. Refresh page.", p);

                if (index >= 0 && index < p.Stops.Count - 1)
                {
                    (p.Stops[index], p.Stops[index + 1]) = (p.Stops[index + 1], p.Stops[index]);
                    Touch(p, userEmail);
                }

                return (true, "", p);
            }
        }

        private static void Touch(GroupPlan p, string userEmail)
        {
            p.Version++;
            p.UpdatedByEmail = userEmail;
            p.UpdatedAtUtc = DateTime.UtcNow;
        }

        // for tests
        public static void ClearForTests()
        {
            lock (_lock) { _plans.Clear(); }
        }
    }
}
