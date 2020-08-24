using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductTermsControl.Insfrastructure.Helpers;
using ProductTermsControl.Insfrastructure.IntarfaceConnReposit;
using ProductTermsControl.Insfrastructure.StartUpExtensions;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ProductTermsControl.WebAPI
{
    
        public class Startup
        {
            private readonly IWebHostEnvironment _env;
            private readonly IConfiguration _configuration;

            public Startup(IWebHostEnvironment env, IConfiguration configuration)
            {
                _env = env;
                _configuration = configuration;
            }

            // This method gets called by the runtime. Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
            {
                // use sql server db in production and sqlite db in development
                if (_env.IsProduction())
                    services.AddDbContext<DataContext>();
                else
                    services.AddDbContext<DataContext>();

                services.AddCors();
                services.AddControllers();
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                // configure strongly typed settings objects
                var appSettingsSection = _configuration.GetSection("AppSettings");
                services.Configure<AppSettings>(appSettingsSection);

                // configure jwt authentication
                var appSettings = appSettingsSection.Get<AppSettings>();
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);

                RegisterServices(services);

                services.AddAuthService(_env, key);



                // configure DI for application services
                //services.AddScoped<IUserService, UserService>();

                //  swagger
                services.AddCustomizedSwagger(_env);
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
            {
                // migrate any database changes on startup (includes initial db creation)
                dataContext.Database.Migrate();
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles();
                app.UseRouting();

                // global cors policy
                app.UseCors(builder => builder
                        .WithOrigins("http://localhost:4200/")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());

                app.UseAuthentication();
                    app.UseAuthorization();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });

                //swagger

                app.UseCustomizedSwagger(_env);
        }
        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            IntarfaceConnReposit.RegisterServices(services);
        }
    }
    }

