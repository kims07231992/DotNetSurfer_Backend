using DotNetSurfer.Core.TokenGenerators;
using DotNetSurfer_Backend.Core.Interfaces.CDNs;
using DotNetSurfer_Backend.Core.Interfaces.Encryptors;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using DotNetSurfer_Backend.Core.Managers;
using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Infrastructure.CDNs;
using DotNetSurfer_Backend.Infrastructure.Encryptors;
using DotNetSurfer_Backend.Infrastructure.Entities;
using DotNetSurfer_Backend.Infrastructure.Repositories;
using DotNetSurfer_Backend.Infrastructure.TokenGenerators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using LogManager = DotNetSurfer_Backend.Core.Managers.LogManager;

namespace DotNetSurfer_Backend.API.Helpers
{
    public static class ServiceConfigurator
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<IEncryptor, HashEncryptor>();
            services.AddSingleton<ITokenGenerator, JwtGenerator>();
            services.AddScoped<ICdnHandler, AzureBlobHandler>();
            services.AddTransient<IAdminManager, AdminManager>();
            services.AddTransient<IAnnouncementManager, AnnouncementManager>();
            services.AddTransient<IArticleManager, ArticleManager>();
            services.AddTransient<IFeatureManager, FeatureManager>();
            services.AddTransient<IHeaderManager, HeaderManager>();
            services.AddTransient<ILogManager, LogManager>();
            services.AddTransient<IProfileManager, ProfileManager>();
            services.AddTransient<IStatusManager, StatusManager>();
            services.AddTransient<ITopicManager, TopicManager>();
            services.AddTransient<IUserManager, UserManager>();
        }

        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DotNetSurferDbContext>((options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            IdentityModelEventSource.ShowPII = true; // X509SecurityKey needs to be at least 1024
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(Permission), policy => policy.RequireRole(PermissionType.Admin.ToString()));
            });
        }

        public static void AddAspDotNetCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            // To avoid loop between primary and foreign keys
            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Session
            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            int sessionExpireMinutes = Convert.ToInt32(configuration["Session:ExpireMinutes"]);
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(sessionExpireMinutes);
                options.Cookie.HttpOnly = true;
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotNetSurfer", Version = "v1" });
            });
        }

        public static void UseExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
        }

        public static void UseAspDotNetCore(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            //app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static void UseSwagger(this IApplicationBuilder app)
        {
            SwaggerBuilderExtensions.UseSwagger(app);
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetSurfer V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
