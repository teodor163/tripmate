using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Pages.Groups
{
    public class ExportModel : PageModel
    {
        public IActionResult OnGet(string groupId, string format = "txt")
        {
            var email = HttpContext.Session.GetString("user_email");
            if (string.IsNullOrEmpty(email))
                return Redirect("/Auth/Login");

            if (!GroupService.IsMember(groupId, email))
                return Forbid();

            var g = GroupService.GetGroup(groupId);
            if (g == null) return NotFound();

            var plan = GroupPlanService.GetOrCreate(groupId);

            format = (format ?? "txt").ToLower().Trim();
            if (format == "md")
            {
                var bytes = ExportService.ToMarkdown(plan, g.Name);
                return File(bytes, "text/markdown", "tripmate_plan.md");
            }
            else
            {
                var bytes = ExportService.ToTxt(plan, g.Name);
                return File(bytes, "text/plain", "tripmate_plan.txt");
            }
        }
    }
}
