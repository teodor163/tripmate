using Microsoft.AspNetCore.Mvc.RazorPages;
using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Pages.Activities
{
    public class NearbyModel : PageModel
    {
        public List<Activity> AllActivities = new();
        public List<Activity> Results = new();

        public string? DestinationQuery { get; set; }

        public void OnGet(string? destination)
        {
            DestinationQuery = destination;

            AllActivities = new()
            {
                // Bled
                new() { Destination = "Bled", Name = "Lake walking path", DistanceKm = 2 },
                new() { Destination = "Bled", Name = "Ojstrica viewpoint", DistanceKm = 3 },
                new() { Destination = "Bled", Name = "Vintgar gorge", DistanceKm = 6 },

                // Ljubljana
                new() { Destination = "Ljubljana", Name = "Tivoli park", DistanceKm = 1 },
                new() { Destination = "Ljubljana", Name = "Castle hiking path", DistanceKm = 2 },
                new() { Destination = "Ljubljana", Name = "Botanical garden", DistanceKm = 4 },

                // Portoroz
                new() { Destination = "Portoroz", Name = "Beach activities", DistanceKm = 1 },
                new() { Destination = "Portoroz", Name = "Strunjan nature reserve", DistanceKm = 5 },

                // Vienna
                new() { Destination = "Vienna", Name = "Schonbrunn gardens", DistanceKm = 3 },
                new() { Destination = "Vienna", Name = "Danube island biking", DistanceKm = 6 },

                
            };

            if (!string.IsNullOrEmpty(destination))
            {
                Results = AllActivities
                    .Where(a => a.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(a => a.DistanceKm)
                    .ToList();
            }
        }
    }
}
