﻿namespace Repository
{
    using Microsoft.EntityFrameworkCore;
    using Repository.Entities;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> AuthUsers { get; set; }

        public DbSet<BalanceSheet> BalanceSheets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = 1,
                    Name = "Administrator"
                },
                new Role
                {
                    RoleId = 2,
                    Name = "User"
                }
            );

            builder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    RoleId = 1,
                    Email = "admin@test.com",
                    Username = "admin",
                    Password = "admin"
                },
                new User
                {
                    UserId = 2,
                    RoleId = 2,
                    Email = "user1@test.com",
                    Username = "user1",
                    Password = "password"
                }
            );

            builder.Entity<BalanceSheet>().HasData(
                new BalanceSheet
                {
                    UserId = 1,
                    Amount = 1300,
                    BalanceSheetId = 1
                },
                new BalanceSheet
                {
                    UserId = 1,
                    Amount = -800,
                    BalanceSheetId = 2
                });

            builder
                .Entity<Role>()
                .Property(p => p.RoleId)
                .ValueGeneratedOnAdd();

            builder
                .Entity<BalanceSheet>()
                .Property(p => p.BalanceSheetId)
                .ValueGeneratedOnAdd();

            builder
                .Entity<User>()
                .Property(p => p.UserId)
                .ValueGeneratedOnAdd();
        }
    }
}