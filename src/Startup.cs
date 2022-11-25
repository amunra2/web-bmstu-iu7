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

            // Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IServerService, ServerService>();
            services.AddTransient<IPlatformService, PlatformService>();
            services.AddTransient<IHostingService, HostingService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<ICountryService, CountryService>();


            // Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IServerRepository, ServerRepository>();
            services.AddTransient<IHostingRepository, HostingRepository>();
            services.AddTransient<IPlatformRepository, PlatformRepository>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();

            // MVC
            services.AddControllers();
            services.AddEndpointsApiExplorer();

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

            // Authentication
            // app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();     // нет определенных маршрутов
            });
        }
    }
}
