﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using ProductTermsControl.Application.Paging.Services;
using ProductTermsControl.Insfrastructure.Repository;
using ProductTermsControl.Application.ApplicationDbContext;
using ProductTermsControl.Insfrastructure.Helpers;
using ProductTermsControl.Application.Services;
using Microsoft.AspNetCore.Authorization;
using ProductTermsControl.Application.Authorization;

namespace ProductTermsControl.Insfrastructure.IntarfaceConnReposit
{
    public class IntarfaceConnReposit
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddScoped<IRepository<UserReference>, Repository<UserReference>>();
            services.AddScoped<IRepository<ReasonForOutOfStock>, Repository<ReasonForOutOfStock>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMagazineService, MagazineService>();
            services.AddScoped<IMagazineBranchService, MagazineBranchService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IResponsiblePersonsGroupService, ResponsiblePersonsGroupService>();
            services.AddScoped<IResponsiblePersonsForProductService, ResponsiblePersonsForProductService>();
            services.AddScoped<IProductToBranchService, ProductToBranchService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthorizationHandler, RoleHandler>();

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
