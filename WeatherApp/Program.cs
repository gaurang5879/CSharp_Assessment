using System.Text.Json;
using WeatherApp;

class Program
{
    private static readonly string apiKey = "6be4b1dabdf907c54ef9e6378e0e0984";
    private static readonly HttpClient client = new();

    static async Task Main()
    {
        Console.Write("Enter city name: ");
        string city = Console.ReadLine();

        string url = $"http://api.weatherstack.com/current?access_key={apiKey}&query={city}";

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            var weather = JsonSerializer.Deserialize<WeatherResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (weather.Success == false && weather.Error != null)
            {
                Console.WriteLine($"Error: {weather.Error.Info} (Code: {weather.Error.Code})");
            }
            else
            {
                Console.WriteLine($"Weather in {city}: {weather.Current.Temperature}°C");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching weather data: {ex.Message}");
        }
    }
}
