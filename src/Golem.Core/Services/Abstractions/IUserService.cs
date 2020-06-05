using System.Security.Claims;
using System.Threading.Tasks;
using Golem.Data.PostgreSql.Models;

namespace Golem.Core.Services.Abstractions
{
    public interface IUserService
    {
        Task<AppUser> GetUserWithTokenByClaimsAsync(ClaimsPrincipal claimsPrincipal);
        Task<AppUser> GetUserWithTokenByIdAsync(string userId);
        Task<AppUser> GetUserWithRefreshTokenAsync(string email);
    }
}
