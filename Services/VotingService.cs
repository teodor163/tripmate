using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Services
{
    public static class VotingService
    {
        private static readonly object _lock = new();
        private static readonly Dictionary<string, VoteState> _votesByGroup = new(); // groupId -> votes

        public static VoteState GetOrCreate(string groupId)
        {
            lock (_lock)
            {
                if (_votesByGroup.TryGetValue(groupId, out var v))
                    return v;

                v = new VoteState { GroupId = groupId };
                _votesByGroup[groupId] = v;
                return v;
            }
        }

        // Vote = pick one stop. Can be changed.
        public static (bool ok, string error) CastVote(string groupId, string userEmail, string stopName)
        {
            if (string.IsNullOrWhiteSpace(stopName))
                return (false, "Invalid stop");

            lock (_lock)
            {
                var v = GetOrCreate(groupId);
                v.UserVotes[userEmail] = stopName;
                return (true, "");
            }
        }

        public static Dictionary<string, int> GetCounts(string groupId)
        {
            lock (_lock)
            {
                var v = GetOrCreate(groupId);
                return v.UserVotes.Values
                    .GroupBy(x => x)
                    .ToDictionary(g => g.Key, g => g.Count());
            }
        }

        public static (bool hasTie, List<string> winners, int topVotes) GetWinners(string groupId)
        {
            var counts = GetCounts(groupId);
            if (counts.Count == 0) return (false, new List<string>(), 0);

            var top = counts.Values.Max();
            var winners = counts.Where(kv => kv.Value == top).Select(kv => kv.Key).OrderBy(x => x).ToList();
            return (winners.Count > 1, winners, top);
        }

        // for tests
        public static void ClearForTests()
        {
            lock (_lock) { _votesByGroup.Clear(); }
        }
    }
}
