using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Teatastic.Areas.Identity.Data;
using Teatastic.Models;

namespace Teatastic.Data
{
    public class SeedDataContext
    {
        public static async Task<IActionResult> Initialize(System.IServiceProvider serviceProvider, UserManager<TeatasticUser> userManager)
        {
            using (var context = new TeatasticContext(serviceProvider.GetRequiredService<DbContextOptions<TeatasticContext>>()))
            {
                context.Database.Migrate();
                context.Database.EnsureCreated();

                if (!context.Roles.Any())
                {

                    TeatasticUser dummy = new TeatasticUser
                    {
                        Email = "john@doe.com",
                        EmailConfirmed = true,
                        LockoutEnabled = true,
                        UserName = "dummy",
                        FirstName = "john",
                        LastName = "doe",
                    };
                    TeatasticUser admin = new TeatasticUser
                    {
                        Email = "admin@teatastic.be",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        UserName = "Administrator",
                        FirstName = "Administrator",
                        LastName = "Teatastic"
                    };

                    await userManager.CreateAsync(admin, "Abc!12345");
                    await userManager.CreateAsync(dummy, "Abc!12345");

                    context.Roles.AddRange
                    (
                       new IdentityRole { Id = "SystemAdministrator", Name = "SystemAdministrator", NormalizedName = "SYSTEMADMINISTRATOR" },
                       new IdentityRole { Id = "UserAdministrator", Name = "UserAdministrator", NormalizedName = "USERADMINISTRATOR" },
                       new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER" }
                    );
                    context.SaveChanges();

                    string id = admin.Id;

                    context.UserRoles.AddRange
                        (
                            new IdentityUserRole<string> { RoleId = "User", UserId = admin.Id },
                            new IdentityUserRole<string> { RoleId = "UserAdministrator", UserId = admin.Id },
                            new IdentityUserRole<string> { RoleId = "SystemAdministrator", UserId = admin.Id }
                        );
                    context.SaveChanges();

                }
                TeatasticUser dummyUser = context.Users.FirstOrDefault(u => u.UserName == "dummy");
                TeatasticUser administrator = context.Users.FirstOrDefault(u => u.UserName == "Administrator");

                #region seeder for models
                if (!context.Function.Any())
                {
                    context.Function.AddRange(
                        new Function { Id=1,Name = "Relaxing" },
                        new Function { Id=2,Name = "Energizing" },
                        new Function { Id=3,Name = "Detoxifying" }
                        );
                    context.SaveChanges();
                }

                if (!context.Brands.Any())
                {
                    context.Brands.AddRange(
                        new Brand { Id= 1,Name = "Liption" },
                        new Brand { Id= 2,Name = "NoonHerb" },
                        new Brand { Id=3,Name = "TiZone" }
                        );
                    context.SaveChanges();
                }

                if (!context.Tea.Any())
                {
                    context.Tea.AddRange(
                        new Tea
                        {
                            Id = 1,
                            Name = "Green Tea",
                            Price = 3.99,
                            BrandId = 1,
                            FunctionIds = new List<int> { 1, 2 }
                        }, 
                        new Tea
                        {
                            Id = 2,
                            Name = "Black Tea",
                            Price = 6.99,
                            BrandId = 1,
                            FunctionIds = new List<int> { 2 }
                        }, 
                        new Tea
                        {
                            Id = 3,
                            Name = "Yellow Tea",
                            Price = 2.99,
                            BrandId = 2,
                            FunctionIds = new List<int> { 1, 3 }
                        }
                        );
                        context.SaveChanges();
                }
                
                
                #endregion
                return null;
            }
        }
    }
}
