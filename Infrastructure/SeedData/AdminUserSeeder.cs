using Application.Interfaces.InternalServices;
using Domain.Abstraction;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.SeedData
{
    public static class AdminUserSeeder
    {
        public static async Task SeedAdminUsersAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var securityService = scope.ServiceProvider.GetRequiredService<IHashingService>();


            if (!context.AdminUsers.Any())
            {
                var adminUser = new AdminUser
                {
                    Email = "admin@system.com",
                    FullName = "System Administrator",
                    Role = "admin",
                    IsActive = true,
                    PasswordHash = securityService.HashPassword("Admin@123"),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await context.AdminUsers.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
