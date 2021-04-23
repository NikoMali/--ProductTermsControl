using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductTermsControl.Insfrastructure;
using ProductTermsControl.Insfrastructure.Helpers;
using ProductTermsControl.Insfrastructure.IntarfaceConnReposit;
using ProductTermsControl.Insfrastructure.StartUpExtensions;
using Serilog;
using System;
using System.Text;


namespace ProductTermsControl.WebAPI
{

    public class Startup
        {
            private readonly IWebHostEnvironment _env;
            private readonly IConfiguration _configuration;
            private AppSettings settings;

            public Startup(IWebHostEnvironment env, IConfiguration configuration)
            {
                _env = env;
                _configuration = configuration;

        }

            // This method gets called by the runtime. Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
            {
                // use sql server db in production and sqlite db in development
                services.AddDbContext<DataContext>();

                /*Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(_configuration, "Serilog")
               //.MinimumLevel.Error()
               .WriteTo.MySQL(connectionString: _configuration.GetConnectionString("WebApiDatabase"))
               .CreateLogger();*/
                Serilogging.SerilogInitial(_configuration);



                services.AddCors();
                services.AddControllers(options => {
                    options.Filters.Add(typeof(UserActivityFilter));
                })
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                // configure strongly typed settings objects
                var appSettingsSection = _configuration.GetSection("AppSettings");
                services.Configure<AppSettings>(appSettingsSection);

                // configure jwt authentication
                settings = appSettingsSection.Get<AppSettings>();
                var key = Encoding.ASCII.GetBytes(settings.Secret);

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
            
           

            // global cors policy
            app.UseCors(builder => builder
                        .WithOrigins(settings.AllowedHost)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        );
                dataContext.Database.Migrate();
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles();
                app.UseRouting();

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

