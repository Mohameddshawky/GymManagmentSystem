using GymManagmentDAL.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.DataSeed
{
    public static class IdentitySeeding
    {
        public static async Task<bool> SeedDataAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                var hasUser = userManager.Users.Any();
                var hasRole = roleManager.Roles.Any();
                if (!hasRole)
                {
                    var roles = new List<IdentityRole>
                    {
                        new IdentityRole { Name = "SuperAdmin" },
                        new IdentityRole { Name = "Admin" }
                    };
                    foreach (var role in roles)
                    {
                        var roleExists = await roleManager.RoleExistsAsync(role.Name!);
                        if (!roleExists)
                        {
                            var result = await roleManager.CreateAsync(role);

                        }
                    }

                }
                if (!hasUser)
                {
                    var superAdminUser = new ApplicationUser
                    {
                        UserName = "MohamedShawky",
                        FirstName = "Mohamed",
                        LastName = "Shawky",
                        Email = "shawky1mohamed2@gmail.com",
                        PhoneNumber = "01113560216",


                    };
                    await userManager.CreateAsync(superAdminUser, "P@ssw0rd");
                    await userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                    var AdminUser = new ApplicationUser
                    {
                        UserName = "SalmaShawky",
                        FirstName = "Salma",
                        LastName = "Shawky",
                        Email = "salma1mohamed2@gmail.com",
                        PhoneNumber = "01113560215",


                    };
                    await userManager.CreateAsync(superAdminUser, "P@ssw0rd");
                    await userManager.AddToRoleAsync(superAdminUser, "Admin");
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
    }
}
