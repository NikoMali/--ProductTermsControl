using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using ProductTermsControl.Application.Paging.Services;
using ProductTermsControl.Insfrastructure.Repository;
using ProductTermsControl.Application.ApplicationDbContext;
using ProductTermsControl.Insfrastructure.Helpers;

namespace ProductTermsControl.Insfrastructure.IntarfaceConnReposit
{
    public class IntarfaceConnReposit
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddScoped<IRepository<UserReference>, Repository<UserReference>>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMagazineRepository, MagazineRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IResponsiblePersonsGroupRepository, ResponsiblePersonsGroupRepository>();
            services.AddScoped<IResponsiblePersonsForProductRepository, ResponsiblePersonsForProductRepository>();
            services.AddScoped<IProductToBranchRepository, ProductToBranchRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();

            //for paging
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            ////
            
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<DataContext>());
        }
    }
}
