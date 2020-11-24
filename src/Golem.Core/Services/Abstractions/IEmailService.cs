using System.Threading.Tasks;
using Golem.Core.Models;
using Golem.Core.Models.Dto.Requests;

namespace Golem.Core.Services.Abstractions
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(EmailModel model);
    }
}
