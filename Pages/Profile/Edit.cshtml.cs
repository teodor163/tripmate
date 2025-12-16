using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Pages.Profile
{
    public class EditModel : PageModel
    {
        public bool IsLoggedIn { get; set; }
        public string Email { get; set; } = "";

        [BindProperty] public string Name { get; set; } = "";
        [BindProperty] public string Interests { get; set; } = "";

        public string Error { get; set; } = "";
        public string Success { get; set; } = "";

        public void OnGet()
        {
            var email = HttpContext.Session.GetString("user_email");
            if (string.IsNullOrEmpty(email)) { IsLoggedIn = false; return; }

            var user = UserStore.FindByEmail(email);
            if (user == null) { IsLoggedIn = false; return; }

            IsLoggedIn = true;
            Email = user.Email;
            Name = user.Name;
            Interests = user.Interests;
        }

        public IActionResult OnPost()
        {
            var email = HttpContext.Session.GetString("user_email");
            if (string.IsNullOrEmpty(email)) { Error = "Not logged in"; return Page(); }

            if (string.IsNullOrWhiteSpace(Name))
            {
                Error = "Invalid name format";
                return Page();
            }

            UserStore.Update(new Models.UserAccount
            {
                Email = email,
                Name = Name.Trim(),
                Interests = Interests.Trim()
            });

            Success = "Profile updated";
            return RedirectToPage();
        }
    }
}
