using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Pages.Auth
{
    public class LoginModel : PageModel
    {
        [BindProperty] public string Email { get; set; } = "";
        [BindProperty] public string Password { get; set; } = "";

        public string Error { get; set; } = "";
        public string Success { get; set; } = "";

        public void OnGet() { }

        public IActionResult OnPost()
        {
            var (ok, error, user) = AuthService.Login(Email, Password);
            if (!ok) { Error = error; return Page(); }

            HttpContext.Session.SetString("user_email", user!.Email);
            Success = "Logged in";
            return Redirect("/Profile/Edit");
        }
    }
}
