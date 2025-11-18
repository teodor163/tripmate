using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TripMate_TeodorLazar.Pages.Stops
{
    public class StopsModel : PageModel
    {
        public List<string> Stops { get; set; } = new();
        public string ItemsQuery { get; set; } = "";

        public void OnGet(
            string? add,
            int? remove,
            int? moveUp,
            int? moveDown,
            string? items)
        {
            // Load items
            if (!string.IsNullOrEmpty(items))
                Stops = items.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            // ADD
            if (!string.IsNullOrWhiteSpace(add))
                Stops.Add(add);

            // REMOVE
            if (remove.HasValue && remove.Value >= 0 && remove.Value < Stops.Count)
                Stops.RemoveAt(remove.Value);

            // MOVE UP
            if (moveUp.HasValue && moveUp.Value > 0)
            {
                var i = moveUp.Value;
                (Stops[i - 1], Stops[i]) = (Stops[i], Stops[i - 1]);
            }

            // MOVE DOWN
            if (moveDown.HasValue && moveDown.Value < Stops.Count - 1)
            {
                var i = moveDown.Value;
                (Stops[i], Stops[i + 1]) = (Stops[i + 1], Stops[i]);
            }

            // Save query
            ItemsQuery = string.Join(",", Stops);
        }
    }
}
