using System.Threading.Tasks;
using Golem.Core.Models;
using Golem.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Golem.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailsController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        /// <summary>
        ///     Sends "et in touch" email
        /// </summary>
        [HttpPost("emails")]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel model)
        {
            var result = await emailService.SendEmail(model);
            return result ? Ok() : StatusCode(500);
        }
    }
}
