using System.Net.Http;
using System.Threading.Tasks;
using Golem.Core.Models.Dto.Responses;
using Newtonsoft.Json;

namespace Golem.Core.Managers
{
    public class LocationManager
    {
        private const string BaseUrl = "http://api.ipstack.com/";
        private const string AccessKey = "bf7f399c6105417288a45435b46a4610";
        private readonly IHttpClientFactory httpClientFactory;

        public LocationManager(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<LocationResponse> GetLocation(string ipAddress)
        {
            var url = $"{BaseUrl}{ipAddress}?access_key={AccessKey}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await httpClientFactory.CreateClient().SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LocationResponse>(json);
            return result;
        }
    }
}
