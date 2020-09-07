namespace ApiRest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Models;
    using Models.Constants;
    using Models.Enums;
    using Repository;
    using Repository.Interfaces;

    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
                .AddScoped<IAuthUserRepository, AuthUserRepository>()
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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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