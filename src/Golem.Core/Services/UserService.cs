using System.Security.Claims;
using System.Threading.Tasks;
using Golem.Core.Services.Abstractions;
using Golem.Data.PostgreSql.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Golem.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<AppUser> GetUserWithTokenByClaimsAsync(ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            return await GetUserWithTokenByIdAsync(userId);
        }

        public async Task<AppUser> GetUserWithTokenByIdAsync(string userId)
        {
            return await userManager.Users
                .Include(u => u.RefreshToken)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<AppUser> GetUserWithRefreshTokenAsync(string email)
        {
            return await userManager.Users
                .Include(u => u.RefreshToken)
                .FirstOrDefaultAsync(r => r.Email == email);
        }
    }
}
