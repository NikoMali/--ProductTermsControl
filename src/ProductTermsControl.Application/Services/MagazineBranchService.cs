using Microsoft.EntityFrameworkCore;
using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using ProductTermsControl.Application.Filter;
using ProductTermsControl.Application.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppException = ProductTermsControl.Application.Helpers.AppException;

namespace ProductTermsControl.Application.Services
{
    public interface IMagazineBranchService
    {
        Task<IEnumerable<MagazineBranch>> GetAll();
        Task<MagazineBranch> Update(MagazineBranch magazineBranch);
        Task<string> Delete(int Id);
        Task<MagazineBranch> GetById(int Id);
        Task<MagazineBranch> Create(MagazineBranch magazineBranch);
        Task<IEnumerable<MagazineBranch>> GetAllByMagazineId(int magazineId);
        Task<GetAllWithPaging<MagazineBranch>> GetAllForPaging(int PageNumber, int PageSize);
        Task<IEnumerable<User>> GetUsersByBranchId(int branchId);
    }

    public class MagazineBranchService : IMagazineBranchService
    {
        private readonly IApplicationDbContext _context;

        public MagazineBranchService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MagazineBranch>> GetAll()
        {
            return await _context.MagazineBranches.ToListAsync();
        }

        public async Task<MagazineBranch> Update(MagazineBranch magazineBranch)
        {
            try
            {
                _context.MagazineBranches.Update(magazineBranch);
                await _context.SaveChangesAsync();
                return magazineBranch;
            }
            catch (Exception)
            {
                throw new AppException(ResultStatus.FAILED);
            }
           
        }

        public async Task<MagazineBranch> GetById(int Id) 
        {
            return await _context.MagazineBranches.FindAsync(Id);
        }

        public async Task<string> Delete(int Id)
        {
            try
            {
                _context.MagazineBranches.Remove(await _context.MagazineBranches.FindAsync(Id));
                await _context.SaveChangesAsync();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
        }

        public async Task<MagazineBranch> Create(MagazineBranch MagazineBranch)
        {
            await _context.MagazineBranches.AddAsync(MagazineBranch);
            await _context.SaveChangesAsync();
            return MagazineBranch;
        }

        public async Task<IEnumerable<MagazineBranch>> GetAllByMagazineId(int magazineId)
        {
            return await _context.MagazineBranches.Where(m => m.MagazineId == magazineId).ToListAsync();
        }

        public async Task<GetAllWithPaging<MagazineBranch>> GetAllForPaging(int PageNumber, int PageSize)
        {

            var validFilter = new PaginationFilter(PageNumber, PageSize);
            var totalRecords = _context.MagazineBranches.CountAsync();
            var pagedData = await _context.MagazineBranches
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();


            var result = new GetAllWithPaging<MagazineBranch>(validFilter, pagedData, await totalRecords);
            return result;
        }
        public async Task<IEnumerable<User>> GetUsersByBranchId(int branchId)
        {
            var branchUsers =  (from UR in _context.UserReferences
                               join U in _context.Users on UR.UserId equals U.Id
                               where UR.MagazineBranchId == branchId
                               select new User(U)).ToListAsync();
            return await branchUsers;
        }
    }
}
