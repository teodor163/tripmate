using Microsoft.AspNetCore.Mvc.RazorPages;
using TripMate_TeodorLazar.Models;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Pages.Share
{
    public class ViewModel : PageModel
    {
        public string Error { get; set; } = "";
        public string GroupName { get; set; } = "";
        public GroupPlan Plan { get; set; } = new();

        public void OnGet(string token)
        {
            var (ok, err, link) = ShareService.Get(token);
            if (!ok || link == null)
            {
                Error = err;
                return;
            }

            var g = GroupService.GetGroup(link.GroupId);
            if (g == null)
            {
                Error = "Group not found";
                return;
            }

            GroupName = g.Name;
            Plan = GroupPlanService.GetOrCreate(link.GroupId);
        }
    }
}
