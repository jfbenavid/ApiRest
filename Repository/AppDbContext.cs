namespace Repository
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Repository.Entities;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<AuthUser> AuthUsers { get; set; }

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

            builder.Entity<AuthUser>().HasData(
                new AuthUser
                {
                    AuthUserId = 1,
                    RoleId = 1,
                    EmailAddress = "admin@test.com",
                    Username = "admin",
                    Password = "admin"
                },
                new AuthUser
                {
                    AuthUserId = 2,
                    RoleId = 2,
                    EmailAddress = "user1@test.com",
                    Username = "user1",
                    Password = "password"
                }
            );
        }
    }
}