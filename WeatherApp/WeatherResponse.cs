namespace WeatherApp
{
    public class WeatherResponse
    {
        public bool Success { get; set; } = true;
        public WeatherCurrent Current { get; set; }
        public WeatherError Error { get; set; }
    }
}
