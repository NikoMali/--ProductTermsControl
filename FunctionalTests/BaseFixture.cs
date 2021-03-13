using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using ProductTermsControl.Insfrastructure.Helpers;
using ProductTermsControl.WebAPI;
using System;
using System.Net.Http;

namespace FunctionalTests
{
    public class BaseFixture : WebApplicationFactory<Startup>
    {
        //public HttpClient Client { get; }
        //public TestServer Server { get; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");

            builder.ConfigureServices(services =>
            {
                //services.AddDbContext<DataContext>();
            });
        }
    }
}

