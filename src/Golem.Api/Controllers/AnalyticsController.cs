using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Golem.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class AnalyticsController : ControllerBase
    {
        [HttpGet("analytics")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
