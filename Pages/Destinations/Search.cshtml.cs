using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TripMate_TeodorLazar.Pages.Destinations
{
    public class SearchModel : PageModel
    {
        public string? Query { get; set; }
        public List<string> Results { get; set; } = new();

        // Simulirani podatki – ker nimamo API-ja ali baze:
        private static readonly List<string> Destinations = new()
        {
            "Ljubljana", "Maribor", "Koper", "Bled", "Portorož",
            "Celje", "Nova Gorica", "Piran", "New york", "Marino","London","Tokyo","Lenart","Celovec" };

        public void OnGet(string? query)
        {
            Query = query;

            if (!string.IsNullOrEmpty(query))
            {
                Results = Destinations
                    .Where(d => d.Contains(query, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }
    }
}
