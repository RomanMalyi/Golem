using System.Net.Http;
using System.Threading.Tasks;

namespace Golem.Core.Managers
{
    public class SearchManager
    {
        private const string BaseUrl = "http://golem.ml/similar";
        private readonly IHttpClientFactory httpClientFactory;

        public SearchManager(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetSimilar(string search)
        {
            var url = $"{BaseUrl}?words={search}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await httpClientFactory.CreateClient().SendAsync(request);
            return await response.Content.ReadAsStringAsync();;
        }
    }
}
