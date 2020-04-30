using System.Threading.Tasks;
using Golem.Core.Models;

namespace Golem.Core.Services.Abstractions
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(EmailModel model);
    }
}
