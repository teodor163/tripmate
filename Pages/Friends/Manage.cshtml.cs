using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TripMate_TeodorLazar.Models;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Pages.Friends
{
    public class ManageModel : PageModel
    {
        public bool IsLoggedIn { get; set; }
        public string UserEmail { get; set; } = "";

        public List<Group> MyGroups { get; set; } = new();
        public List<FriendInvite> Incoming { get; set; } = new();
        public List<FriendInvite> Outgoing { get; set; } = new();

        public string Error { get; set; } = "";
        public string Success { get; set; } = "";

        public void OnGet(string? accept, string? reject)
        {
            LoadBase();

            if (!IsLoggedIn) return;

            if (!string.IsNullOrEmpty(accept))
            {
                var (ok, err) = FriendService.Respond(accept, UserEmail, accept: true);
                if (!ok) Error = err; else Success = "Invite accepted";
            }

            if (!string.IsNullOrEmpty(reject))
            {
                var (ok, err) = FriendService.Respond(reject, UserEmail, accept: false);
                if (!ok) Error = err; else Success = "Invite rejected";
            }

            LoadLists();
        }

        public IActionResult OnPost(string toEmail, string groupId)
        {
            LoadBase();
            if (!IsLoggedIn) return Redirect("/Auth/Login");

            var (ok, err, _) = FriendService.SendInvite(UserEmail, toEmail, groupId);
            if (!ok) Error = err; else Success = "Invite sent";

            LoadLists();
            return Page();
        }

        private void LoadBase()
        {
            var email = HttpContext.Session.GetString("user_email");
            if (string.IsNullOrEmpty(email))
            {
                IsLoggedIn = false;
                return;
            }

            IsLoggedIn = true;
            UserEmail = email;

            GroupService.EnsureDefaultGroup(UserEmail);
            MyGroups = GroupService.GetGroupsFor(UserEmail);
        }

        private void LoadLists()
        {
            Incoming = FriendService.GetIncoming(UserEmail);
            Outgoing = FriendService.GetOutgoing(UserEmail);
        }
    }
}
