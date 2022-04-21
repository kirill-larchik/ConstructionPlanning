using ConstructionPlanning.WebApplication.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ConstructionPlanning.WebApplication.Data
{
    public class UserService
    {
        internal static async Task InitializeRolesAndUserAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleNames = new string[] { "Admin", "User" };

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            await CreateUser(userManager, Roles.Admin, configuration);
        }

        private static async Task CreateUser(UserManager<User> userManager, string userRole, IConfiguration configuration)
        {
            // Creates a new user.
            var newUser = new User
            {
                UserName = configuration[$"Users:{userRole}:Email"],
                Email = configuration[$"Users:{userRole}:Email"],
                Forename = configuration[$"Users:{userRole}:Forename"],
                Surname = configuration[$"Users:{userRole}:Surname"],
                IsAdmin = userRole == Roles.Admin ? true : false,
            };

            var newUserPassword = configuration[$"Users:{userRole}:Password"];

            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user == null)
            {
                var createUser = await userManager.CreateAsync(newUser, newUserPassword);
                if (createUser.Succeeded)
                {
                    user = await userManager.FindByEmailAsync(newUser.Email);
                    await userManager.AddToRoleAsync(user, userRole);
                }
            }
        }
    }
}
