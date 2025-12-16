using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TripMate_TeodorLazar.Models;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Pages.Groups
{
    public class PlanModel : PageModel
    {
        public bool IsLoggedIn { get; set; }
        public string UserEmail { get; set; } = "";

        public string GroupId { get; set; } = "";
        public string GroupName { get; set; } = "";
        public GroupPlan Plan { get; set; } = new();

        public string Error { get; set; } = "";

        public string ShareUrl { get; set; } = "";
        public string ShareToken { get; set; } = "";

        public void OnGet(string groupId, string? action, int? index, int? clientVersion, int? share, string? disableToken)
        {
            if (!LoadAuth()) return;

            GroupId = groupId;
            var g = GroupService.GetGroup(groupId);
            if (g == null || !GroupService.IsMember(groupId, UserEmail))
            {
                Error = "No access to group";
                return;
            }

            GroupName = g.Name;
            Plan = GroupPlanService.GetOrCreate(groupId);

            // optional: create share
            if (share == 1)
            {
                var link = ShareService.Create(groupId, TimeSpan.FromHours(1));
                ShareToken = link.Token;
                ShareUrl = $"{Request.Scheme}://{Request.Host}/Share/View?token={link.Token}";
            }

            // optional: disable share
            if (!string.IsNullOrEmpty(disableToken))
            {
                ShareService.Disable(disableToken);
            }

            // edit actions via GET (simpler)
            if (!string.IsNullOrEmpty(action) && clientVersion.HasValue)
            {
                var v = clientVersion.Value;

                if (action == "remove" && index.HasValue)
                {
                    var (ok, err, plan) = GroupPlanService.RemoveStop(groupId, v, UserEmail, index.Value);
                    if (!ok) Error = err;
                    Plan = plan;
                }

                if (action == "up" && index.HasValue)
                {
                    var (ok, err, plan) = GroupPlanService.MoveUp(groupId, v, UserEmail, index.Value);
                    if (!ok) Error = err;
                    Plan = plan;
                }

                if (action == "down" && index.HasValue)
                {
                    var (ok, err, plan) = GroupPlanService.MoveDown(groupId, v, UserEmail, index.Value);
                    if (!ok) Error = err;
                    Plan = plan;
                }
            }
        }

        public IActionResult OnPost(string groupId, int clientVersion, string stop)
        {
            if (!LoadAuth()) return Redirect("/Auth/Login");

            GroupId = groupId;
            if (!GroupService.IsMember(groupId, UserEmail))
            {
                Error = "No access";
                return Page();
            }

            var (ok, err, plan) = GroupPlanService.AddStop(groupId, clientVersion, UserEmail, stop);
            if (!ok) Error = err;

            var g = GroupService.GetGroup(groupId);
            GroupName = g?.Name ?? "Group";
            Plan = plan;

            return Page();
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

            // ensure at least one group exists
            GroupService.EnsureDefaultGroup(UserEmail);

            return true;
        }
    }
}
