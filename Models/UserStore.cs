using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Services
{
  
    public static class UserStore
    {
        private static readonly List<UserAccount> Users = new();

        public static bool EmailExists(string email) =>
            Users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        public static void Add(UserAccount user) => Users.Add(user);

        public static UserAccount? FindByEmail(string email) =>
            Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        public static void Update(UserAccount updated)
        {
            var u = FindByEmail(updated.Email);
            if (u == null) return;
            u.Name = updated.Name;
            u.Interests = updated.Interests;
            if (!string.IsNullOrWhiteSpace(updated.Password))
                u.Password = updated.Password;
        }

        // Za teste (da lahko resetiraš)
        public static void ClearForTests() => Users.Clear();
    }
}

