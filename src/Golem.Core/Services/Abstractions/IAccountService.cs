using System.Threading.Tasks;
using Golem.Core.Models.Dto.Responses;
using Golem.Data.PostgreSql.Models;

namespace Golem.Core.Services.Abstractions
{
    public interface IAccountService
    {
        Task<AuthorizationResponse> GetAuthorizationResponse(AppUser user);

        bool IsRefreshTokenValid(string refreshToken, AppUser user);
    }
}
