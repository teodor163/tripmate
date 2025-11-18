using Microsoft.AspNetCore.Mvc.RazorPages;
using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Pages.Weather
{
    public class CheckModel : PageModel
    {
        public WeatherInfo? CurrentWeather { get; set; }

        public string? InputDestination { get; set; }
        public string? ManualCondition { get; set; }

        private static readonly Dictionary<string, string> DefaultWeather = new()
        {
            { "Ljubljana", "sunny" },
            { "Bled", "cloudy" },
            { "Bohinj", "rain" },
            { "Piran", "sunny" },
            { "Portoroz", "sunny" },
            { "Maribor", "cloudy" },
            { "Koper", "sunny" },
            { "Kranjska Gora", "snow" },
            { "Vienna", "cloudy" },
            { "Paris", "rain" },
            { "Tokyo", "sunny" },
            { "Sydney", "sunny" }
        };

        public void OnGet(string? destination, string? condition)
        {
            InputDestination = destination;
            ManualCondition = condition;

            if (string.IsNullOrEmpty(destination))
                return;

            string finalCondition;

          
            if (!string.IsNullOrEmpty(condition))
                finalCondition = condition.ToLower().Trim();
            else
            {
               
                if (!DefaultWeather.TryGetValue(destination, out finalCondition))
                {
                    string[] randomWeather =
                    {
                        "sunny", "cloudy", "rain", "storm", "snow"
                    };
                    finalCondition = randomWeather[new Random().Next(randomWeather.Length)];
                }
            }

            int temp = finalCondition switch
            {
                "sunny" => 24,
                "cloudy" => 18,
                "rain" => 14,
                "storm" => 12,
                "snow" => -2,
                _ => 20
            };

            CurrentWeather = new WeatherInfo
            {
                Destination = destination,
                Condition = finalCondition,
                Temperature = temp
            };
        }
    }
}
