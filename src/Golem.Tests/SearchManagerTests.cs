using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Golem.Core.Managers;
using Moq;
using Moq.Protected;
using Xunit;

namespace Golem.Tests
{
    public class SearchManagerTests
    {
        private readonly SearchManager searchManager;

        public SearchManagerTests()
        {
            var httpFactory = new Mock<IHttpClientFactory>();
            var messageHandler = new Mock<HttpMessageHandler>();
            messageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("response"),
                });

            var client = new HttpClient(messageHandler.Object);
            httpFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            searchManager = new SearchManager(httpFactory.Object);
        }
        
        [Fact]
        public async void ShouldGetLocationResponse()
        {
            var result = await searchManager.GetSimilar("word");
            result.Should().Be("response");
        }
        
        [Theory]
        [InlineData("search query")]
        [InlineData("home")]
        [InlineData("Shop online")]
        [InlineData("offensive security Courses and Certifications")]
        public async void ShouldGetLocation(string words)
        {
            var result = await searchManager.GetSimilar(words);
            result.Should().NotBeNull();
        }
    }
}
