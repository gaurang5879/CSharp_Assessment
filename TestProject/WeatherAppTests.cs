using Moq;
using Moq.Protected;
using System.Net;

namespace WeatherTestProject
{
    [TestFixture]
    public class WeatherAppTests
    {
        private const string SampleJsonResponse = "{" +
            "\"location\": { \"name\": \"London\", \"country\": \"United Kingdom\" }," +
            "\"current\": { \"temperature\": 25, \"weather_descriptions\": [\"Clear sky\"]}" +
            "}";

        private Mock<HttpMessageHandler> mockHandler;
        private HttpClient httpClient;

        [SetUp]
        public void Setup()
        {
            mockHandler = new Mock<HttpMessageHandler>();
            httpClient = new HttpClient(mockHandler.Object);
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

            var weatherFetcher = new WeatherFetcher(httpClient);
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
        public void FetchWeatherAsync_InvalidCity_ShouldThrowHttpRequestException()
        {
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("City not found"));

            var weatherFetcher = new WeatherFetcher(httpClient);

            Assert.ThrowsAsync<HttpRequestException>(async () => await weatherFetcher.FetchWeatherAsync("InvalidCity"));
        }
    }
}