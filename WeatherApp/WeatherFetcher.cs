using System.Text.Json;
using System.Text.Json.Nodes;
using WeatherApp;

public class WeatherFetcher
{
    private readonly HttpClient _httpClient;
    private static string _apiKey;
    private static string _baseUrl;

    /// <summary>
    /// Constructor with optional API Key & Base URL for testing
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="apiKey"></param>
    /// <param name="baseUrl"></param>
    public WeatherFetcher(HttpClient httpClient, string? apiKey = null, string? baseUrl = null)
    {
        _httpClient = httpClient;

        if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(baseUrl))
        {
            _apiKey = apiKey;
            _baseUrl = baseUrl;
        }
        else
        {
            LoadConfiguration();
        }
    }

    /// <summary>
    /// Load api key from app settings
    /// </summary>
    private static void LoadConfiguration()
    {
        string configPath = "appsettings.json";
        if (File.Exists(configPath))
        {
            var json = File.ReadAllText(configPath);
            var jsonNode = JsonNode.Parse(json);
            _apiKey = jsonNode?["WeatherAPI"]?["ApiKey"]?.ToString();
            _baseUrl = jsonNode?["WeatherAPI"]?["BaseUrl"]?.ToString();
        }

        if (string.IsNullOrWhiteSpace(_apiKey) && !IsTestEnvironment())
        {
            Console.Write(Messages.EnterApiKey);
            _apiKey = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(_apiKey))
            {
                File.WriteAllText(configPath, $"{{\"WeatherAPI\":{{\"ApiKey\":\"{_apiKey}\", \"BaseUrl\": \"{_baseUrl ?? "http://api.weatherstack.com/current"}\"}}}}");
            }
        }
    }

    /// <summary>
    /// Determines if the application is running inside a test environment
    /// </summary>
    /// <returns>True if running in test environment</returns>
    private static bool IsTestEnvironment()
    {
        return AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName.StartsWith("test", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Main method
    /// </summary>
    /// <returns></returns>
    public static async Task Main()
    {
        using HttpClient client = new HttpClient();
        var weatherFetcher = new WeatherFetcher(client);

        Console.Write(Messages.EnterCity);
        string city = Console.ReadLine()?.Trim();

        Console.Write(Messages.EnterCountry);
        string country = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(city))
        {
            Console.WriteLine(Messages.EmptyCityError);
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
                Console.WriteLine(string.Format(Messages.WeatherInfo, weatherData.Location.Name, weatherData.Location.Country));
                Console.WriteLine(string.Format(Messages.TemperatureInfo, weatherData.Current.Temperature));
                Console.WriteLine(string.Format(Messages.DescriptionInfo, string.Join(", ", weatherData.Current.WeatherDescriptions ?? new string[] { })));
            }
            else
            {
                Console.WriteLine(Messages.WeatherError);
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(Messages.NetworkError + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(Messages.UnexpectedError + ex.Message);
        }
    }


    /// <summary>
    /// Fetches weather data from the Weather stack API.
    /// </summary>
    /// <param name="city">The city name.</param>
    /// <returns>A WeatherResponse object containing weather details.</returns>
    public async Task<WeatherResponse?> FetchWeatherAsync(string city)
    {
        string url = $"{_baseUrl}?access_key={_apiKey}&query={city}";
        try
        {
            var response = await _httpClient.GetStringAsync(url);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<WeatherResponse>(response, options);
        }
        catch (HttpRequestException)
        {
            return null;
        }
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
