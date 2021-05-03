using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductTermsControl.Application.ApplicationDbContext;
using ProductTermsControl.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTermsControl.Application.Authorization
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement()
        {
           
        }
        public RoleRequirement(string permission)
        {
            Permission = permission;
        }

        public string Permission { get; }
    }

    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly IUserService _context;
        //UserManager<IdentityUser> _userManager;
        //RoleManager<IdentityRole> _roleManager;

        public RoleHandler(IUserService context 
            //,UserManager<IdentityUser> userManager, 
            //RoleManager<IdentityRole> roleManager
            )
        {

            _context = context;
            //_userManager = userManager;
            //_roleManager = roleManager;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context,RoleRequirement requirement)
        {
            var resourceString = (context.Resource.ToString().Split(' '));
            var getControllerWithAction = resourceString[0].Split('.');
            var getRoleName = getControllerWithAction[2].Replace("Controller","") + "/" + getControllerWithAction[3];

            var validRole = context.User.IsInRole(getRoleName);
            //var user = await _userManager.GetUserAsync(context.User);
            //var userRoleNames = await _userManager.GetRolesAsync(user);
            //var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));

            var hasPermission =await _context.GetById(1);
            if (validRole != true)
            {
                context.Fail();
            }

            context.Succeed(requirement);
        }
    }
}
