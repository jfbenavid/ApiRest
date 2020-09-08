namespace ApiRest
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Repository;

    /// <summary>
    /// Class that inicialize the project.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method to create the project.
        /// Ensures that the database is created before it is needed.
        /// </summary>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    context.Database.EnsureCreatedAsync().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error creating database");
                }
            }

            host.Run();
        }

        /// <summary>
        /// Creates a host builder, configuring the web application.
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}