using Microsoft.AspNetCore.Mvc.RazorPages;
using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Pages.Lodging
{
    public class LodgingModel : PageModel
    {
        public List<Accommodation> All { get; set; } = new();
        public List<Accommodation> Results { get; set; } = new();

        public string? DestinationQuery { get; set; }
        public bool EcoOnly { get; set; }

        public void OnGet(string? destination, bool eco = false)
        {
            DestinationQuery = destination;
            EcoOnly = eco;

            All = new List<Accommodation>
            {
                // Ljubljana
                new() { DestinationName = "Ljubljana", Name = "Green Stay Ljubljana", IsEco = true,  Rating = 4 },
                new() { DestinationName = "Ljubljana", Name = "City Hotel Ljubljana",  IsEco = false, Rating = 3 },

                // Bled
                new() { DestinationName = "Bled",      Name = "Eco Resort Bled",       IsEco = true,  Rating = 5 },
                new() { DestinationName = "Bled",      Name = "Hotel Park Bled",       IsEco = false, Rating = 4 },

                // Bohinj
                new() { DestinationName = "Bohinj",    Name = "Bohinj Eco Lodge",      IsEco = true,  Rating = 5 },
                new() { DestinationName = "Bohinj",    Name = "Lake View Bohinj",      IsEco = false, Rating = 4 },

                // Portoroz
                new() { DestinationName = "Portoroz",  Name = "Portoroz Green Villa",  IsEco = true,  Rating = 4 },
                new() { DestinationName = "Portoroz",  Name = "Hotel Portoroz",        IsEco = false, Rating = 3 },

                // Piran
                new() { DestinationName = "Piran",     Name = "Piran Eco Apartments",  IsEco = true,  Rating = 4 },
                new() { DestinationName = "Piran",     Name = "Piran Seaside Hotel",   IsEco = false, Rating = 3 },

                // Maribor
                new() { DestinationName = "Maribor",   Name = "Maribor Green Inn",     IsEco = true,  Rating = 4 },
                new() { DestinationName = "Maribor",   Name = "Central Hotel Maribor", IsEco = false, Rating = 3 },

                // Koper
                new() { DestinationName = "Koper",     Name = "Koper Eco Rooms",       IsEco = true,  Rating = 4 },
                new() { DestinationName = "Koper",     Name = "Koper Marina Hotel",    IsEco = false, Rating = 3 },

                // Kranjska Gora
                new() { DestinationName = "Kranjska Gora", Name = "Alpine Eco Lodge",  IsEco = true,  Rating = 5 },
                new() { DestinationName = "Kranjska Gora", Name = "Ski Resort Hotel",  IsEco = false, Rating = 4 },

                // Vienna
                new() { DestinationName = "Vienna",    Name = "Eco Gardens Vienna",    IsEco = true,  Rating = 5 },
                new() { DestinationName = "Vienna",    Name = "Hilton Vienna Park",    IsEco = false, Rating = 4 },

                // Paris
                new() { DestinationName = "Paris",     Name = "Paris Green Hotel",     IsEco = true,  Rating = 4 },
                new() { DestinationName = "Paris",     Name = "Hotel Eiffel Center",   IsEco = false, Rating = 4 },

                // Barcelona
                new() { DestinationName = "Barcelona", Name = "Barcelona Eco Suites",  IsEco = true,  Rating = 4 },
                new() { DestinationName = "Barcelona", Name = "La Rambla Hotel",       IsEco = false, Rating = 3 },

                // Tokyo
                new() { DestinationName = "Tokyo",     Name = "Tokyo Green Palace",    IsEco = true,  Rating = 4 },
                new() { DestinationName = "Tokyo",     Name = "Shinjuku Central Hotel",IsEco = false, Rating = 3 },

                // Sydney
                new() { DestinationName = "Sydney",    Name = "Sydney Nature Resort",  IsEco = true,  Rating = 5 },
                new() { DestinationName = "Sydney",    Name = "Sydney Harbor Hotel",   IsEco = false, Rating = 4 }
            };

            Results = All
                .Where(a => string.IsNullOrEmpty(DestinationQuery) ||
                            a.DestinationName.Contains(DestinationQuery, StringComparison.OrdinalIgnoreCase))
                .Where(a => !EcoOnly || a.IsEco)
                .ToList();
        }
    }
}
