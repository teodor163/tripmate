using Microsoft.AspNetCore.Mvc.RazorPages;
using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Pages.Destinations
{
    public class SearchModel : PageModel
    {
        public string? Query { get; set; }
        public List<Destination> Results { get; set; } = new();
        public string[] SelectedCategories { get; set; } = Array.Empty<string>();

        public List<Destination> All = new()
        {
            // Slovenija
            new() { Name = "Ljubljana",      Country = "Slovenia",        Category = "culture" },
            new() { Name = "Bled",           Country = "Slovenia",        Category = "nature" },
            new() { Name = "Bohinj",         Country = "Slovenia",        Category = "nature" },
            new() { Name = "Portoroz",       Country = "Slovenia",        Category = "food" },
            new() { Name = "Piran",          Country = "Slovenia",        Category = "culture" },
            new() { Name = "Maribor",        Country = "Slovenia",        Category = "food" },
            new() { Name = "Koper",          Country = "Slovenia",        Category = "culture" },
            new() { Name = "Kranjska Gora",  Country = "Slovenia",        Category = "sports" },

            // evropa
            new() { Name = "Vienna",         Country = "Austria",         Category = "culture" },
            new() { Name = "Salzburg",       Country = "Austria",         Category = "nature" },
            new() { Name = "Munich",         Country = "Germany",         Category = "culture" },
            new() { Name = "Berlin",         Country = "Germany",         Category = "culture" },
            new() { Name = "Zagreb",         Country = "Croatia",         Category = "culture" },
            new() { Name = "Split",          Country = "Croatia",         Category = "nature" },
            new() { Name = "Dubrovnik",      Country = "Croatia",         Category = "culture" },
            new() { Name = "Paris",          Country = "France",          Category = "culture" },
            new() { Name = "Nice",           Country = "France",          Category = "nature" },
            new() { Name = "Barcelona",      Country = "Spain",           Category = "food" },
            new() { Name = "Madrid",         Country = "Spain",           Category = "culture" },
            new() { Name = "Rome",           Country = "Italy",           Category = "culture" },
            new() { Name = "Milan",          Country = "Italy",           Category = "food" },

            // svet
            new() { Name = "New York",       Country = "USA",             Category = "culture" },
            new() { Name = "Los Angeles",    Country = "USA",             Category = "culture" },
            new() { Name = "Tokyo",          Country = "Japan",           Category = "food" },
            new() { Name = "Kyoto",          Country = "Japan",           Category = "culture" },
            new() { Name = "Sydney",         Country = "Australia",       Category = "nature" },
            new() { Name = "Melbourne",      Country = "Australia",       Category = "culture" }
        };

        public void OnGet(string? query, string[]? categories)
        {
            Query = query ?? "";
            SelectedCategories = categories ?? Array.Empty<string>();

            Results = All
                .Where(d => string.IsNullOrEmpty(Query) ||
                            d.Name.Contains(Query, StringComparison.OrdinalIgnoreCase))
                .Where(d => SelectedCategories.Length == 0 ||
                            SelectedCategories.Contains(d.Category))
                .ToList();
        }
    }
}
