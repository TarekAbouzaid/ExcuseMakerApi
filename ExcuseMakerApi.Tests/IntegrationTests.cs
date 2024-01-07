using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExcuseMakerApi.Tests.IntegrationTests
{
    [TestFixture]
    public class ExcuseControllerIntegrationTests
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            // Initialize HttpClient
            _client = new HttpClient();

            // Set the base address to the running API
            _client.BaseAddress = new System.Uri("http://localhost:5208");
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        [Test]
        public async Task GetRandomExcuse_ReturnsExcuse()
        {
            // Arrange
            string url = "/api/excuse/random?category=2";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            // Optionally deserialize the response content and make further assertions
        }

        [Test]
        public async Task GetRandomExcuse_ReturnsNotFoundForInvalidCategory()
        {
            // Arrange
            string url = "/api/excuse/random?category=InvalidCategory";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}