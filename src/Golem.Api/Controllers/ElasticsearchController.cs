using System.Threading.Tasks;
using Golem.Data.Elasticsearch.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Golem.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class ElasticsearchController : ControllerBase
    {
        private readonly ElasticClient _elasticClient;

        public ElasticsearchController(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        /// <summary>
        ///     Search project
        /// </summary>
        [HttpGet("elasticsearch")]
        public async Task<IActionResult> Search([FromQuery] string searchTerm)
        {
            var result = await
                _elasticClient.SearchAsync<Project>(s =>
                    s.Query(q => q
                            .MultiMatch(m => m
                                .Fields(f => f.Field(p => p.Title).Field(p => p.Description))
                                .Query(searchTerm)
                                .Fuzziness(Fuzziness.EditDistance(1))
                            )
                        )
                        .Take(5)
                );

            return Ok(result.Documents.Count != 0
                ? result.Documents
                : (await _elasticClient.SearchAsync<Project>(s => s.Take(5))).Documents);
        }
    }
}
