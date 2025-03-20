namespace WeatherApp
{
    public static class Messages
    {
        public const string EnterCity = "Enter city name: ";
        public const string EnterCountry = "Enter country name (optional, press Enter to skip): ";
        public const string EmptyCityError = "City name cannot be empty.";
        public const string EnterApiKey = "Enter your Weather API Key: ";
        public const string NetworkError = "Network error: ";
        public const string UnexpectedError = "Unexpected error: ";
        public const string WeatherError = "Error fetching weather data. The location may not be supported.";
        public const string WeatherInfo = "\nWeather in {0}, {1}";
        public const string TemperatureInfo = "Temperature: {0}°C";
        public const string DescriptionInfo = "Description: {0}";
    }
}
