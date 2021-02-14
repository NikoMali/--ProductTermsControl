using Microsoft.EntityFrameworkCore;
using ProductTermsControl.Application.ApplicationDbContext;
using ProductTermsControl.Application.Filter;
using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTermsControl.Application.Services
{
    public interface IMagazineService
    {
        Task<IEnumerable<Magazine>> GetAll();
        Task<Magazine> Update(Magazine magazine);
        Task<string> Delete(int Id);
        Task<Magazine> GetById(int Id);
        Task<Magazine> Create(Magazine magazine);
        Task<GetAllWithPaging<Magazine>> GetAllForPaging(int PageNumber, int PageSize);
    }

    public class MagazineService : IMagazineService
    {
        private readonly IApplicationDbContext _context;

        public MagazineService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Magazine>> GetAll()
        {
            return await _context.Magazines.ToListAsync();
        }

        public async Task<Magazine> Update(Magazine magazine)
        {
            try
            {
                _context.Magazines.Update(magazine);
                await _context.SaveChangesAsync();
                return magazine;
            }
            catch (Exception)
            {
                throw new AppException(ResultStatus.FAILED);
            }
           
        }

        public async Task<Magazine> GetById(int Id) 
        {
            return await _context.Magazines.FindAsync(Id);
        }

        public async Task<string> Delete(int Id)
        {
            try
            {
                _context.Magazines.Remove(await _context.Magazines.FindAsync(Id));
                await _context.SaveChangesAsync();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                throw new AppException(ResultStatus.FAILED);
            }
        }

        public async Task<Magazine> Create(Magazine magazine)
        {
            await _context.Magazines.AddAsync(magazine);
            await _context.SaveChangesAsync();
            return magazine;
        }

        public async Task<GetAllWithPaging<Magazine>> GetAllForPaging(int PageNumber, int PageSize)
        {

            var validFilter = new PaginationFilter(PageNumber, PageSize);
            var totalRecords = _context.Companys.CountAsync();
            var pagedData = await _context.Magazines
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();


            var result = new GetAllWithPaging<Magazine>(validFilter, pagedData, await totalRecords);
            return result;
        }


    }
}
