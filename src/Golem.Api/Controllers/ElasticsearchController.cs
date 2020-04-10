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
        public IActionResult Search([FromQuery] string searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var result =
                    _elasticClient.Search<Project>(s =>
                        s.Query(q => q
                                .MultiMatch(m => m
                                    .Fields(p => p
                                        .Field("Title")
                                        .Field("Description"))
                                    .Query(searchTerm)
                                    .Fuzziness(Fuzziness.EditDistance(1))
                                )
                            )
                            .Take(10)
                    );

                return Ok(result);
            }

            return BadRequest();
        }
    }
}
