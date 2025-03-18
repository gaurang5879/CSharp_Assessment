using System.Text.Json;

class Program
{
    private static readonly string apiKey = "YOUR_OPENWEATHERMAP_API_KEY"; // Replace with actual API Key
    private static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        Console.Write("Enter city name: ");
        string city = Console.ReadLine();

        string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        try
        {
            var response = await client.GetStringAsync(url);
            var weather = JsonSerializer.Deserialize<WeatherResponse>(response);
            Console.WriteLine($"Weather in {city}: {weather.Main.Temp}°C, {weather.Weather[0].Description}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching weather data: {ex.Message}");
        }
    }
}

class WeatherResponse
{
    public WeatherMain Main { get; set; }
    public WeatherDescription[] Weather { get; set; }
}

class WeatherMain
{
    public float Temp { get; set; }
}

class WeatherDescription
{
    public string Description { get; set; }
}
