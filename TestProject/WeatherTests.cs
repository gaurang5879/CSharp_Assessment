using Moq;
using Moq.Protected;
using System.Net;

namespace TestProject
{
    [TestFixture]
    public class WeatherTests
    {
        private const string SampleJsonResponse = "{" +
            "\"location\": { \"name\": \"London\", \"country\": \"United Kingdom\" }," +
            "\"current\": { \"temperature\": 25, \"weather_descriptions\": [\"Clear sky\"]}" +
            "}";

        private Mock<HttpMessageHandler> mockHandler;
        private HttpClient httpClient;
        private WeatherFetcher weatherFetcher;

        [SetUp]
        public void Setup()
        {
            mockHandler = new Mock<HttpMessageHandler>();
            httpClient = new HttpClient(mockHandler.Object);
            weatherFetcher = new WeatherFetcher(httpClient, "mocked_api_key", "http://mocked-url.com");
        }


        [Test]
        public async Task FetchWeatherAsync_ValidCity_ReturnsMockedWeatherResponse()
        {
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(SampleJsonResponse)
                });

            var response = await weatherFetcher.FetchWeatherAsync("London");

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Location);
            Assert.AreEqual("London", response.Location.Name);
            Assert.AreEqual("United Kingdom", response.Location.Country);
            Assert.IsNotNull(response.Current);
            Assert.AreEqual(25, response.Current.Temperature);
            Assert.AreEqual("Clear sky", response.Current.WeatherDescriptions[0]);
        }

        [Test]
        public async Task FetchWeatherAsync_InvalidCity_ReturnsNull()
        {
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("{\"error\": {\"code\": 615, \"type\": \"request_failed\", \"info\": \"City not found\"}}")
                });

            var response = await weatherFetcher.FetchWeatherAsync("InvalidCity");

            Assert.IsNull(response);
        }

        [Test]
        public async Task FetchWeatherAsync_ApiFailure_ReturnsNull()
        {
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("API not reachable"));

            var response = await weatherFetcher.FetchWeatherAsync("New York");

            Assert.IsNull(response);
        }
    }
}
