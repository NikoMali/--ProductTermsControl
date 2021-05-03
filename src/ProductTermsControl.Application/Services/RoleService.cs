using Microsoft.EntityFrameworkCore;
using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using ProductTermsControl.Application.Filter;
using ProductTermsControl.Application.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppException = ProductTermsControl.Application.Helpers.AppException;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ProductTermsControl.Application.Services
{
    public interface IRoleService
    {
        Task<List<string>> GetRolesAsync(int userId);
        Task<IEnumerable<string>> GetAllActionMethodAsync();
    }

    public class RoleService : IRoleService
    {
        private readonly IApplicationDbContext _context;

        public RoleService(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<string>> GetRolesAsync(int userId)
        {

            var result = new List<string>();

            var getRole =await (from UR in _context.UserRoles
                         join R in _context.Roles on UR.RoleId equals R.Id
                         where UR.UserId == userId
                         select new
                         {
                            Role = R.Name,
                            Permissions = R.Permissions
                         }).FirstOrDefaultAsync();

            result.Add(getRole.Role);
            if (getRole.Permissions.Length > 0)
            {
                result.AddRange(getRole.Permissions.Split(';'));
            }

            return  result;
        }

        public Task<IEnumerable<string>> GetAllActionMethodAsync()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            var controlleractionlist = asm.GetTypes()
                                         .Where(type => typeof(Controller).IsAssignableFrom(type))
                                         .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                                         .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                                         .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                                         .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            return Task.FromResult(controlleractionlist.Select(x=> x.Controller.Replace("Controller", "") + "/" + x.Action));
        }




    }
}
