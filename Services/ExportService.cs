using System.Text;
using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Services
{
    public static class ExportService
    {
        public static byte[] ToTxt(GroupPlan plan, string groupName)
        {
            var sb = new StringBuilder();
            sb.AppendLine("TripMate plan export (TXT)");
            sb.AppendLine($"Group: {groupName}");
            sb.AppendLine($"Version: {plan.Version}");
            sb.AppendLine($"UpdatedBy: {plan.UpdatedByEmail}");
            sb.AppendLine($"UpdatedAtUtc: {plan.UpdatedAtUtc:u}");
            sb.AppendLine();
            sb.AppendLine("Stops:");
            for (int i = 0; i < plan.Stops.Count; i++)
                sb.AppendLine($"{i + 1}. {plan.Stops[i]}");
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public static byte[] ToMarkdown(GroupPlan plan, string groupName)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# TripMate plan export (MD)");
            sb.AppendLine();
            sb.AppendLine($"**Group:** {groupName}");
            sb.AppendLine($"**Version:** {plan.Version}");
            sb.AppendLine($"**UpdatedBy:** {plan.UpdatedByEmail}");
            sb.AppendLine($"**UpdatedAtUtc:** {plan.UpdatedAtUtc:u}");
            sb.AppendLine();
            sb.AppendLine("## Stops");
            for (int i = 0; i < plan.Stops.Count; i++)
                sb.AppendLine($"- {i + 1}. {plan.Stops[i]}");
            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
