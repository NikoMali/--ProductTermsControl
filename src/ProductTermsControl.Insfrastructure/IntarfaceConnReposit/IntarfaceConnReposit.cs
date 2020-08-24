using Microsoft.Extensions.DependencyInjection;
using ProductTermsControl.Application.Services;
using ProductTermsControl.Domain.Interfaces;
using ProductTermsControl.Insfrastructure.Repository;


namespace ProductTermsControl.Insfrastructure.IntarfaceConnReposit
{
    public class IntarfaceConnReposit
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IMagazineRepository, MagazineRepository>();
            services.AddScoped<IMagazineService, MagazineService>();

            services.AddScoped<IMagazineBranchRepository, MagazineBranchRepository>();
            services.AddScoped<IMagazineBranchService, MagazineBranchService>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyService, CompanyService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IResponsiblePersonsGroupRepository, ResponsiblePersonsGroupRepository>();
            services.AddScoped<IResponsiblePersonsGroupService, ResponsiblePersonsGroupService>();

            services.AddScoped<IResponsiblePersonsForProductRepository, ResponsiblePersonsForProductRepository>();
            services.AddScoped<IResponsiblePersonsForProductService, ResponsiblePersonsForProductService>();

            services.AddScoped<IProductToBranchRepository, ProductToBranchRepository>();
            services.AddScoped<IProductToBranchService, ProductToBranchService>();

            services.AddScoped<ICommonRepository, CommonRepository>();

        }
    }
}
