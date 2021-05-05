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
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAll();
        Task<Company> Update(Company company);
        Task<string> Delete(int Id);
        Task<Company> GetById(int Id);
        Task<Company> Create(Company company);
        Task<GetAllWithPaging<Company>> GetAllForPaging(int PageNumber, int PageSize);
    }

    public class CompanyService : ICompanyService
    {
        private readonly IApplicationDbContext _context;

        public CompanyService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _context.Companys.ToListAsync();
        }

        public async Task<Company> Update(Company Company)
        {
            try
            {
                _context.Companys.Update(Company);
                await _context.SaveChangesAsync();
                return Company;
            }
            catch (Exception)
            {
                throw new AppException(ResultStatus.FAILED);
            }
           
        }

        public async Task<Company> GetById(int Id) 
        {
            return await _context.Companys.FindAsync(Id);
        }

        public async Task<string> Delete(int Id)
        {
            try
            {
                _context.Companys.Remove(await _context.Companys.FindAsync(Id));
                await _context.SaveChangesAsync();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
        }

        public async Task<Company> Create(Company Company)
        {
            await _context.Companys.AddAsync(Company);
            await _context.SaveChangesAsync();
            return Company;
        }
        public async Task<GetAllWithPaging<Company>> GetAllForPaging(int PageNumber, int PageSize)
        {
            
            var validFilter = new PaginationFilter(PageNumber, PageSize);
            var totalRecords = _context.Companys.CountAsync();
            if (PageSize == 0)
            {
                validFilter.PageSize = await totalRecords;
            }
            var pagedData = 
                (from C in await _context.Companys.ToListAsync()
                 join P in _context.Products on C.Id equals P.CompanyId
                 select new
                 {
                     C,
                     P
                 } into CP
                 group CP by CP.P.CompanyId into gP
                 select new Company(gP.FirstOrDefault().C,gP.Select(x=>x.P).ToList()))
                .OrderBy(x=>x.Id)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            
            var result = new GetAllWithPaging<Company>(validFilter, pagedData,await totalRecords);
            return result;
        }
        
    }
}
