using Microsoft.Extensions.DependencyInjection;
using ProductTermsControl.Application.Services;



namespace ProductTermsControl.Application.IntarfaceConnReposit
{
    public class IntfConRepoForBusinessLogic
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddScoped<IRepository<UserReference>, Repository<UserReference>>();
            
            services.AddScoped<IUserService, UserService>();

            
            services.AddScoped<IMagazineService, MagazineService>();

            
            services.AddScoped<IMagazineBranchService, MagazineBranchService>();

           
            services.AddScoped<ICompanyService, CompanyService>();

            
            services.AddScoped<IProductService, ProductService>();

           
            services.AddScoped<IResponsiblePersonsGroupService, ResponsiblePersonsGroupService>();

            
            services.AddScoped<IResponsiblePersonsForProductService, ResponsiblePersonsForProductService>();

            
            services.AddScoped<IProductToBranchService, ProductToBranchService>();

            
            
        }
    }
}
