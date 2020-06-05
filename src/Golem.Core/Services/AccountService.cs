using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Golem.Core.Models.Dto.Responses;
using Golem.Core.Models.Settings;
using Golem.Core.Services.Abstractions;
using Golem.Data.PostgreSql.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Golem.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly JwtSettings jwtSettings;
        private readonly UserManager<AppUser> userManager;

        public AccountService(UserManager<AppUser> userManager,
            JwtSettings jwtSettings)
        {
            this.userManager = userManager;
            this.jwtSettings = jwtSettings;
        }

        public async Task<AuthorizationResponse> GetAuthorizationResponse(AppUser user)
        {
            var jwtTuple = GenerateJwtToken(user);
            var refreshToken = await GetRefreshTokenAsync(user);

            var result = new AuthorizationResponse
            {
                RefreshToken = refreshToken.Token
            };
            (result.AccessToken, result.AccessTokenExpirationDateTimeOffset) = await jwtTuple;

            return result;
        }

        public bool IsRefreshTokenValid(string refreshToken, AppUser user)
        {
            if (refreshToken == null || user?.RefreshToken == null) return false;
            return refreshToken.Equals(user.RefreshToken.Token) &&
                   user.RefreshToken.TokenExpiration > DateTimeOffset.Now;
        }

        private async Task<(string, DateTime)> GenerateJwtToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var userRoles = await userManager.GetRolesAsync(user);
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings.JwtExpireMinutes));

            var token = new JwtSecurityToken(
                null,
                null,
                claims,
                DateTime.UtcNow,
                expires,
                credentials
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expires);
        }

        private async Task<RefreshToken> GetRefreshTokenAsync(AppUser user)
        {
            if (!RefreshTokenShouldBeReset(user.RefreshToken)) return user.RefreshToken;

            user.RefreshToken = GenerateRefreshToken(user.Id);
            var updateResult = await userManager.UpdateAsync(user);

            if (updateResult.Succeeded) return user.RefreshToken;

            throw new Exception(updateResult.Errors.ToString());
        }

        private bool RefreshTokenShouldBeReset(RefreshToken token)
        {
            if (token == null) return true;

            var expirationTimeLeftMinutes = (token.TokenExpiration - DateTimeOffset.UtcNow).TotalMinutes;

            return expirationTimeLeftMinutes <
                   Convert.ToDouble(jwtSettings.RefreshTokenExpireMinutes) / 2;
        }

        private RefreshToken GenerateRefreshToken(string userId)
        {
            var result = new RefreshToken
            {
                TokenExpiration =
                    DateTimeOffset.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings.RefreshTokenExpireMinutes)),
                AppUserId = userId
            };

            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            result.Token = Convert.ToBase64String(randomNumber);
            return result;
        }
    }
}
