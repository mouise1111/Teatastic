﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                        UserName = "john@doe.com",
                        FirstName = "john",
                        LastName = "doe",
                    };
                    TeatasticUser admin = new TeatasticUser
                    {
                        Email = "admin@teatastic.be",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        UserName = "admin@teatastic.be",
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
                            new IdentityUserRole<string> { RoleId = "User", UserId = dummy.Id },
                            new IdentityUserRole<string> { RoleId = "UserAdministrator", UserId = admin.Id },
                            new IdentityUserRole<string> { RoleId = "SystemAdministrator", UserId = admin.Id }
                        );
                    context.SaveChanges();

                }
                TeatasticUser dummyUser = context.Users.FirstOrDefault(u => u.UserName == "dummy");
                TeatasticUser administrator = context.Users.FirstOrDefault(u => u.UserName == "Administrator");


                #region seeder for models

                Function f1 = new Function { Name = "Relaxing" };
                Function f2 = new Function { Name = "Energizing" };
                Function f3 = new Function { Name = "Detoxifying" };

                if (!context.Function.Any())
                {
                    context.Function.AddRange(f1, f2, f3);
                    context.SaveChanges();
                }

                if (!context.Brands.Any())
                {
                    context.Brands.AddRange(
                        new Brand { Name = "Liption" },
                        new Brand { Name = "NoonHerb" },
                        new Brand { Name = "TiZone" }
                        );
                    context.SaveChanges();
                }

                if (!context.Tea.Any())
                {
                    context.Tea.AddRange(
                        new Tea
                        {
                            Name = "Mango Tea",
                            Price = 3.99,
                            BrandId = 1,
                            Functions = new List<Function>
                            {
                                f1,
                                f2
                            }
                        },
                        new Tea
                        {
                            Name = "Green Tea",
                            Price = 3.99,
                            BrandId = 1,
                            Functions = new List<Function> { f1, f2 }
                        },
                        new Tea
                        {
                            Name = "Black Tea",
                            Price = 6.99,
                            BrandId = 1,
                            Functions = new List<Function> { f2 }
                        },
                        new Tea
                        {
                            Name = "Yellow Tea",
                            Price = 2.99,
                            BrandId = 2,
                            Functions = new List<Function> { f1, f3 }
                        },
                        new Tea
                        {
                            Name = "White Tea",
                            Price = 4.99,
                            BrandId = 2,
                            Functions = new List<Function> { f1 }
                        },
                        new Tea
                        {
                            Name = "Oolong Tea",
                            Price = 5.99,
                            BrandId = 3,
                            Functions = new List<Function> { f2, f3 }
                        },
                        new Tea
                        {
                            Name = "Pu-erh Tea",
                            Price = 8.99,
                            BrandId = 1,
                            Functions = new List<Function> { f1, f2, f3 }
                        },
                        new Tea
                        {
                            Name = "Darjeeling Tea",
                            Price = 6.49,
                            BrandId = 3,
                            Functions = new List<Function> { f2 }
                        },
                        new Tea
                        {
                            Name = "Assam Tea",
                            Price = 4.49,
                            BrandId = 1,
                            Functions = new List<Function> { f1, f3 }
                        },
                        new Tea
                        {
                            Name = "Ceylon Tea",
                            Price = 7.99,
                            BrandId = 2,
                            Functions = new List<Function> { f1, f2 }
                        },
                        new Tea
                        {
                            Name = "Earl Grey Tea",
                            Price = 5.99,
                            BrandId = 3,
                            Functions = new List<Function> { f3 }
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
