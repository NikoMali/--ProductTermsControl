using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ProductTermsControl.Insfrastructure;
using ProductTermsControl.Insfrastructure.Helpers;
using ProductTermsControl.Insfrastructure.IntarfaceConnReposit;
using ProductTermsControl.Insfrastructure.StartUpExtensions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

            //add localize language 
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ka-GE")
                };
                options.DefaultRequestCulture = new RequestCulture(culture: "ka-GE", uiCulture: "ka-GE");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                /*options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    // Order is important, its in which order they will be evaluated
                    new CookieRequestCultureProvider(),
                    new QueryStringRequestCultureProvider()
                };*/

                var defaultCookieRequestProvider =
                    options.RequestCultureProviders.FirstOrDefault(rcp =>
                        rcp.GetType() == typeof(CookieRequestCultureProvider));
                if (defaultCookieRequestProvider != null)
                    options.RequestCultureProviders.Remove(defaultCookieRequestProvider);

                options.RequestCultureProviders.Insert(0,
                    new CookieRequestCultureProvider()
                    {
                        CookieName = ".AspNetCore.Culture",
                        Options = options
                    });
            });
            ////////////////////////////////////////////////////////////////
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
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseMiddleware<ErrorHandlerMiddleware>();

            // global cors policy
            app.UseCors(builder => builder
                        .WithOrigins(settings.AllowedHost)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        );
                dataContext.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /*//////////// add localize language
            var supportedCultures = new[] { "ka-GE","en-US" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[1])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            // Find the cookie provider with LINQ
            var cookieProvider = localizationOptions.RequestCultureProviders
                .OfType<CookieRequestCultureProvider>()
                .First();
            // Set the new cookie name
            cookieProvider.CookieName = "UserCulture";

            app.UseRequestLocalization(localizationOptions);
            ///////////////////*/



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

