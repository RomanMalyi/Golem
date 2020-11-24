using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Golem.Core.Exceptions;
using Golem.Core.Models.Dto.Requests;
using Golem.Core.Models.Dto.Responses;
using Golem.Core.Services.Abstractions;
using Golem.Data.PostgreSql.Models;
using Golem.Data.PostgreSql.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Golem.Api.Controllers
{
    [ApiController]
    [Route("api/account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly RefreshTokenRepository refreshTokenRepository;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IUserService userService;

        public AccountController(SignInManager<AppUser> signInManager,
            IUserService userService,
            IAccountService accountService,
            RefreshTokenRepository refreshTokenRepository)
        {
            this.signInManager = signInManager;
            this.userService = userService;
            this.accountService = accountService;
            this.refreshTokenRepository = refreshTokenRepository;
        }

        /// <summary>
        ///     Login for a registered user
        /// </summary>
        ///<remarks>
        ///   "AdminCredentials": {"Email": "admin@gmail.com","Password": "admin1"}
        /// </remarks>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthorizationResponse>> Login([FromBody] LoginUserRequest model)
        {
            model.Email = model.Email.ToLower();
            var user = await userService.GetUserWithRefreshTokenAsync(model.Email);
            if (user == null)
                throw AppExceptions.EmailNotFound();

            var signInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (signInResult.IsNotAllowed)
                throw AppExceptions.EmailNotConfirmed();
            if (!signInResult.Succeeded)
                throw AppExceptions.WrongPassword();

            return Ok(await accountService.GetAuthorizationResponse(user));
        }

        /// <summary>
        ///     Returns a new access token
        /// </summary>
        [HttpPost("account/refresh-access-token")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<AuthorizationResponse>> RefreshAccessToken(
            [FromBody] RefreshAccessTokenRequest request)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(request.AccessToken);
            var userId = token.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

            var user = await userService.GetUserWithTokenByIdAsync(userId);
            if (user == null) throw AppExceptions.UserNotFound($"user id: {userId}");
            if (user.RefreshTokenId == null)
                throw new AppException(400, $"Can't refresh token for logged out user with id: {userId}.",
                    "Can't refresh token for logged out user.");

            if (accountService.IsRefreshTokenValid(request.RefreshToken, user))
                return Ok(await accountService.GetAuthorizationResponse(user));

            throw new AppException(409, "Refresh token is not valid.", "Please login.");
        }

        /// <summary>
        ///     Logout from all user's devices
        /// </summary>
        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout()
        {
            var user = await userService.GetUserWithTokenByClaimsAsync(User);
            if (user == null) throw AppExceptions.UserNotFound(User);

            if (user.RefreshTokenId == null) throw AppExceptions.AlreadyLoggedOut();
            await refreshTokenRepository.Delete(user.RefreshToken);

            await signInManager.SignOutAsync();
            return Ok(new { });
        }
    }
}
