using Microsoft.AspNetCore.Mvc.RazorPages;
using TripMate_TeodorLazar.Models;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Pages.Groups
{
    public class MyGroupsModel : PageModel
    {
        public bool IsLoggedIn { get; set; }
        public List<Group> Groups { get; set; } = new();

        public void OnGet()
        {
            var email = HttpContext.Session.GetString("user_email");
            if (string.IsNullOrEmpty(email))
            {
                IsLoggedIn = false;
                return;
            }

            IsLoggedIn = true;
            GroupService.EnsureDefaultGroup(email);
            Groups = GroupService.GetGroupsFor(email);
        }
    }
}
