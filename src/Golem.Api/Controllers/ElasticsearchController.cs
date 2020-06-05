using System.Collections.Generic;
using System.Threading.Tasks;
using Golem.Core.Managers;
using Golem.Data.Elasticsearch.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Golem.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ElasticsearchController : ControllerBase
    {
        private readonly ElasticClient elasticClient;
        private readonly SearchManager searchManager;

        public ElasticsearchController(ElasticClient elasticClient,
            SearchManager searchManager)
        {
            this.elasticClient = elasticClient;
            this.searchManager = searchManager;
        }

        /// <summary>
        ///     Search project
        /// </summary>
        [HttpGet("elasticsearch")]
        public async Task<ActionResult<List<Project>>> Search([FromQuery] string searchTerm)
        {
            var result = await SearchAsync(searchTerm);

            if (result.Documents.Count == 0)
            {
               var similar = await searchManager.GetSimilar(searchTerm);
               result = await SearchAsync(similar);
            }

            return Ok(result.Documents.Count != 0
                ? result.Documents
                : (await elasticClient.SearchAsync<Project>(s => s.Take(5))).Documents);
        }

        private async Task<ISearchResponse<Project>> SearchAsync(string searchTerm)
        {
            return await
                elasticClient.SearchAsync<Project>(s =>
                    s.Query(q => q
                            .MultiMatch(m => m
                                .Fields(f => f.Field(p => p.Title).Field(p => p.Description))
                                .Query(searchTerm)
                                .Fuzziness(Fuzziness.EditDistance(1))
                            )
                        )
                        .Take(6)
                );
        }
    }
}
