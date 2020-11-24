using System;
using System.Threading.Tasks;
using Golem.Data.PostgreSql.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Golem.Data.PostgreSql
{
    public class SeedData
    {
         public static async Task Initialize(IServiceProvider serviceProvider, string adminPassword, string adminEmail)
        {
            await using var context =
                new GolemContext(serviceProvider.GetRequiredService<DbContextOptions<GolemContext>>());

            var adminId = await EnsureUser(serviceProvider, adminPassword, adminEmail);
            await EnsureRole(serviceProvider, adminId, "Admin");
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
            string adminPassword, string adminEmail)
        {
            var userManager = serviceProvider.GetService<UserManager<AppUser>>();

            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                user = new AppUser
                {
                    FirstName = "Golem",
                    LastName = "Admin",
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, adminPassword);
                if (!result.Succeeded)
                    throw new Exception("Admin not created");
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task EnsureRole(IServiceProvider serviceProvider,
            string userId, string role)
        {
            IdentityResult identityResult = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                identityResult = await roleManager.CreateAsync(new IdentityRole(role));
                if (!identityResult.Succeeded)
                    throw new Exception("Role is not created");
            }

            var userManager = serviceProvider.GetService<UserManager<AppUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("Admin not found!");
            }

            await userManager.AddToRoleAsync(user, role);
        }
    }
}
