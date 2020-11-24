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
    public class LocationManagerTests
    {
        private const string response =
            @"{'ip':'188.163.29.28','type':'ipv4','continent_code':'EU','continent_name':'Europe','country_code':'UA','country_name':'Ukraine','region_code':'26','region_name':'Ivano-Frankivsk','city':'Kalush','zip':'77315','latitude':49.04090118408203,'longitude':24.40329933166504,'location':{'geoname_id':707099,'capital':'Kyiv','languages':[{'code':'uk','name':'Ukrainian','native':'\u0423\u043a\u0440\u0430\u0457\u043d\u0441\u044c\u043a\u0430'}],'country_flag':'http:\/\/assets.ipstack.com\/flags\/ua.svg','country_flag_emoji':'\ud83c\uddfa\ud83c\udde6','country_flag_emoji_unicode':'U+1F1FA U+1F1E6','calling_code':'380','is_eu':false}}";
        private readonly LocationManager locationManager;

        public LocationManagerTests()
        {
            var httpFactory = new Mock<IHttpClientFactory>();
            var messageHandler = new Mock<HttpMessageHandler>();
            messageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(response),
                });

            var client = new HttpClient(messageHandler.Object);
            httpFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            locationManager = new LocationManager(httpFactory.Object);
        }
        
        [Fact]
        public async void ShouldGetLocationResponse()
        {
            var result = await locationManager.GetLocation("188.163.29.28");
            result.City.Should().Be("Kalush");
        }
        
        [Theory]
        [InlineData("198.51.100.0")]
        [InlineData("203.0.113.0")]
        [InlineData("240.0.0.0")]
        [InlineData("198.18.0.0")]
        public async void ShouldGetLocation(string ip)
        {
            var result = await locationManager.GetLocation(ip);
            result.Should().NotBeNull();
        }
    }
}
