using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Pages.Groups
{
    public class VoteModel : PageModel
    {
        public bool IsLoggedIn { get; set; }
        public string UserEmail { get; set; } = "";

        public string GroupId { get; set; } = "";
        public string GroupName { get; set; } = "";

        public List<string> Stops { get; set; } = new();
        public Dictionary<string, int> Counts { get; set; } = new();

        public bool HasTie { get; set; }
        public List<string> Winners { get; set; } = new();
        public int TopVotes { get; set; }

        public string Error { get; set; } = "";
        public string Success { get; set; } = "";

        public void OnGet(string groupId)
        {
            if (!LoadAuth()) return;

            GroupId = groupId;

            var g = GroupService.GetGroup(groupId);
            if (g == null || !GroupService.IsMember(groupId, UserEmail))
            {
                Error = "No access";
                return;
            }

            GroupName = g.Name;

            var plan = GroupPlanService.GetOrCreate(groupId);
            Stops = plan.Stops.ToList();

            Counts = VotingService.GetCounts(groupId);
            var (hasTie, winners, top) = VotingService.GetWinners(groupId);
            HasTie = hasTie;
            Winners = winners;
            TopVotes = top;
        }

        public IActionResult OnPost(string groupId, string stopName)
        {
            if (!LoadAuth()) return Redirect("/Auth/Login");

            GroupId = groupId;

            if (!GroupService.IsMember(groupId, UserEmail))
            {
                Error = "No access";
                return Page();
            }

            var plan = GroupPlanService.GetOrCreate(groupId);
            if (!plan.Stops.Any(s => s.Equals(stopName, StringComparison.OrdinalIgnoreCase)))
            {
                Error = "Stop does not exist";
                return RedirectToPage(new { groupId });
            }

            var (ok, err) = VotingService.CastVote(groupId, UserEmail, stopName);
            if (!ok) Error = err; else Success = "Vote saved (you can change vote any time)";

            return RedirectToPage(new { groupId });
        }

        private bool LoadAuth()
        {
            var email = HttpContext.Session.GetString("user_email");
            if (string.IsNullOrEmpty(email))
            {
                IsLoggedIn = false;
                return false;
            }

            IsLoggedIn = true;
            UserEmail = email;

            GroupService.EnsureDefaultGroup(UserEmail);
            return true;
        }
    }
}
