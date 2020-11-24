using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Golem.Core.Models.Settings;
using Golem.Core.Services;
using Golem.Core.Services.Abstractions;
using Golem.Data.PostgreSql.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace Golem.Tests
{
    public class AccountServiceTests
    {
        private readonly IAccountService accountService;

        public AccountServiceTests()
        {
            var identityResult = new Mock<IdentityResult>();
            IList<string> roles = new List<string>(new[] {"Admin"});;
            var settings = new JwtSettings
            {
                JwtExpireMinutes = "100",
                JwtKey = "SOME_RANDOM_KEY_DO_NOT_SHARE",
                RefreshTokenExpireMinutes = "1000",
            };
            var userManager = new Mock<UserManager<AppUser>>(MockBehavior.Default);
            userManager.Setup(manager => manager.GetRolesAsync(It.IsAny<AppUser>()))
                .Returns(Task.FromResult(roles));
            userManager.Setup(manager => manager.UpdateAsync(It.IsAny<AppUser>()))
                .Returns(Task.FromResult(identityResult.Object));

            this.accountService = new AccountService(userManager.Object, settings);
        }

        [Fact]
        public void RefreshTokenShouldBeInvalid()
        {
            var result = accountService.IsRefreshTokenValid(null, null);
            result.Should().BeFalse();
        }
    }
}
