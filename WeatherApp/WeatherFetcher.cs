using System.Text.Json;
using WeatherApp;

public class WeatherFetcher(HttpClient httpClient)
{
    private static readonly string apiKey = "your_api_key";
    private readonly HttpClient _httpClient = httpClient;

    public static async Task Main()
    {
        using HttpClient client = new HttpClient();
        var weatherFetcher = new WeatherFetcher(client);

        Console.Write("Enter city name: ");
        string city = Console.ReadLine();

        Console.Write("Enter country name (optional, press Enter to skip): ");
        string country = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(city))
        {
            Console.WriteLine("City name cannot be empty.");
            return;
        }

        city = CapitalizeFirstLetter(city);
        if (!string.IsNullOrWhiteSpace(country))
        {
            country = CapitalizeFirstLetter(country);
            city += $", {country}";
        }

        try
        {
            var weatherData = await weatherFetcher.FetchWeatherAsync(city);
            if (weatherData?.Current != null && weatherData.Location != null)
            {
                Console.WriteLine($"\nWeather in {weatherData.Location.Name}, {weatherData.Location.Country}");
                Console.WriteLine($"Temperature: {weatherData.Current.Temperature}°C");
                Console.WriteLine($"Description: {string.Join(", ", weatherData.Current.WeatherDescriptions ?? new string[] { })}");
            }
            else
            {
                Console.WriteLine("Error fetching weather data.");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Network error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    /// <summary>
    /// Fetches weather data from the Weather stack API.
    /// </summary>
    /// <param name="city">The city name.</param>
    /// <returns>A WeatherResponse object containing weather details.</returns>
    public async Task<WeatherResponse?> FetchWeatherAsync(string city)
    {
        string url = $"http://api.weatherstack.com/current?access_key={apiKey}&query={city}";
        var response = await _httpClient.GetStringAsync(url);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<WeatherResponse>(response, options);
    }

    /// <summary>
    /// Capitalizes the first letter of a given string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>The formatted string with the first letter capitalized.</returns>
    private static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        return char.ToUpper(input[0]) + input.Substring(1).ToLower();
    }
}
