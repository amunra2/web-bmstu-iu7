using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Logging;

using Microsoft.EntityFrameworkCore;
using ServerING.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using ServerING.Services;
using ServerING.Interfaces;
using ServerING.Repository;

namespace ServerING {
    public class Startup {

        private IConfigurationRoot _configuration;

        public Startup(IWebHostEnvironment hostEnv) {
            _configuration = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }

        // This method gets called by the runtime
        public void ConfigureServices(IServiceCollection services) {

            // Connect to DB
            var provider = _configuration["Database"];

            services.AddDbContext<AppDBContent>(
                options => _ = provider switch {
                    "Postgres" => options.UseNpgsql(
                        _configuration.GetConnectionString("DefaultConnection")
                        ),
                    _ => throw new Exception($"Unsupported provider: {provider}")
                }
            );

            // Authentication path
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            // Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IServerService, ServerService>();
            services.AddTransient<IPlatformService, PlatformService>();
            services.AddTransient<IWebHostingService, WebHostingService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<ICountryService, CountryService>();


            // Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IServerRepository, ServerRepository>();
            services.AddTransient<IWebHostingRepository, WebHostingRepository>();
            services.AddTransient<IPlatformRepository, PlatformRepository>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();

            // MVC
            services.AddControllersWithViews();

            // Swagger
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory) {
            if (env.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Logger
            // loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logs"));

            // Authentication
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
