using System.Text.Json.Serialization;

namespace WeatherApp
{
    public class WeatherCurrent
    {
        public int Temperature { get; set; }
        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }
        [JsonPropertyName("weather_icons")]
        public string[]? WeatherIcons { get; set; }
        [JsonPropertyName("weather_descriptions")]
        public string[]? WeatherDescriptions { get; set; }
    }
}
