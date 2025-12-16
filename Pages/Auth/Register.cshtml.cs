using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        [BindProperty] public string Email { get; set; } = "";
        [BindProperty] public string Password { get; set; } = "";
        [BindProperty] public string Name { get; set; } = "";

        public string Error { get; set; } = "";
        public bool Success { get; set; }

        public void OnGet() { }

        public void OnPost()
        {
            var (ok, error) = AuthService.Register(Email, Password, Name);
            if (!ok) { Error = error; Success = false; return; }
            Success = true;
        }
    }
}
