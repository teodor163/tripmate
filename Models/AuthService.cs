using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Services
{
    public static class AuthService
    {
        public static (bool ok, string error) Register(string email, string password, string name)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(name))
                return (false, "Missing data");

            if (!email.Contains("@"))
                return (false, "Invalid email format");

            if (UserStore.EmailExists(email))
                return (false, "Email already used");

            UserStore.Add(new UserAccount
            {
                Email = email.Trim(),
                Password = password,
                Name = name.Trim(),
                Interests = ""
            });

            return (true, "");
        }

        public static (bool ok, string error, UserAccount? user) Login(string email, string password)
        {
            var u = UserStore.FindByEmail(email ?? "");
            if (u == null) return (false, "Account does not exist", null);
            if (u.Password != password) return (false, "Wrong password", null);
            return (true, "", u);
        }
    }
}
