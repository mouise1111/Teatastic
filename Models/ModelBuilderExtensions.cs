using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Teatastic.Areas.Identity.Data;

namespace Teatastic.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Crear ROLES
            List<IdentityRole> roles = new List<IdentityRole>() {
                new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
                new IdentityRole { Name = "Coach", NormalizedName = "COACH" },
                new IdentityRole { Name = "Swimmer", NormalizedName = "SWIMMER" },
                new IdentityRole { Name = "Visitor", NormalizedName = "VISITOR" }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // Create USERS
            List<TeatasticUser> users = new List<TeatasticUser>() {
                new TeatasticUser {
                    UserName = "admin@tea.com",
                    NormalizedUserName = "ADMIN@TEA.COM",
                    Email = "admin@tea.com",
                    NormalizedEmail = "ADMIN@TEA.COM",
                    FirstName= "Admin",
                    LastName = "Aaa"
                },
                new TeatasticUser {
                    UserName = "user@tea.com",
                    NormalizedUserName = "USER@TEA.COM",
                    Email = "USER@tea.com",
                    NormalizedEmail = "USER@TEA.COM",
                    FirstName= "John",
                    LastName = "Doe"
                }
            };
            modelBuilder.Entity<TeatasticUser>().HasData(users);

            // Add passwords to users
            var passwordHasher = new PasswordHasher<TeatasticUser>();
            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "Teatastic01#");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "Teatastic01#");

            // Add roles to users
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();
            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == "Administrator").Id
            });
            roles.Add(new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" });

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[1].Id,
                RoleId = roles.First(q => q.Name == "Client").Id
            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);

        }
    }
}
