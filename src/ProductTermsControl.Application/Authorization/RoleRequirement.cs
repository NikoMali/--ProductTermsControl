using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductTermsControl.Application.ApplicationDbContext;
using ProductTermsControl.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductTermsControl.Application.Authorization
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement(string permission)
        {
            Permission = permission;
        }

        public string Permission { get; }
    }

    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly IUserService _context;

        public RoleHandler(IUserService context)
        {


            _context = context;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context,RoleRequirement requirement)
        {
           

            var hasPermission =await _context.GetById(1);
            if (hasPermission == null)
            {
                context.Succeed(requirement);
            }
            
        }
    }
}
