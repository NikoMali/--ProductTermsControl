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

namespace ProductTermsControl.Application.Services
{
    public interface IRoleService
    {
        
    }

    public class RoleService : IRoleService
    {
        private readonly IApplicationDbContext _context;

        public RoleService(IApplicationDbContext context)
        {
            _context = context;
        }
        
        
    }
}
