namespace ApiRest
{
    using System.IO;
    using System.Text;
    using AutoMapper;
    using Domain;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.PlatformAbstractions;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Models;
    using Models.Constants;
    using Models.Enums;
    using Repository;
    using Repository.Interfaces;

    /// <summary>
    /// Class to configure all the application.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Creates a new instance of <see cref="Startup"/>
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configure all the services in the web app.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseInMemoryDatabase("Test");
            });

            services.AddCors(config =>
            {
                config.AddPolicy("CorsPolicy", builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .Build());
            });

            services
                .Configure<JwtConfigModel>(_configuration.GetSection("JwtConfig"));

            services
                .AddScoped<IUserRepository, UserRepository>()
                .AddTransient<IJwtUtils, Utilities>();

            services.AddAutoMapper(typeof(Startup));

            services
                .AddAuthorization(config =>
                {
                    config.AddPolicy(
                        Policies.Admin,
                        policy =>
                        {
                            policy.RequireRole(
                                Roles.Administrator.ToString());
                        });

                    config.AddPolicy(
                        Policies.NonAdmin,
                        policy =>
                        {
                            policy.RequireRole(
                                Roles.Administrator.ToString(),
                                Roles.User.ToString());
                        });
                });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(config =>
                {
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:SecretKey"])),
                        ValidIssuer = _configuration["JwtConfig:Issuer"],
                        ValidAudience = _configuration["JwtConfig:Issuer"],
                    };
                });

            services.AddControllers();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Jose Fabian Benavides", Version = "v1" });
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "",
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "ApiRest.xml");
                config.IncludeXmlComments(filePath);
            });
        }

        /// <summary>
        /// Configure all the middlewares in the application.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(config =>
            {
                config.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}