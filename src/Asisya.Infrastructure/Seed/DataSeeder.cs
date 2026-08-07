using Asisya.Domain.Entities;
using Asisya.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Asisya.Infrastructure.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await context.Database.MigrateAsync();

        if (!await context.Categories.AnyAsync())
        {
            context.Categories.AddRange(
                new Category
                {
                    CategoryName = "SERVIDORES",
                    Description = "Productos para servidores",
                    Picture = "https://images.server.com/server.png"
                },
                new Category
                {
                    CategoryName = "CLOUD",
                    Description = "Productos cloud",
                    Picture = "https://images.server.com/cloud.png"
                });
        }

        if (!await context.Suppliers.AnyAsync())
        {
            context.Suppliers.Add(
                new Supplier
                {
                    CompanyName = "ASISYA",
                    ContactName = "Administrador",
                    ContactTitle = "Administrador",
                    Country = "Colombia",
                    City = "Bogotá"
                });
        }

        if (!context.Users.Any())
        {
            context.Users.Add(new User
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123*"),
                Role = "Admin"
            });

            await context.SaveChangesAsync();
        }

        await context.SaveChangesAsync();
    }
}