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
        Task<IEnumerable<UserReference>> GetUsersByBranchId(int branchId);
        Task<int> EmployeOfNumbers(int magazineBranchId);
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
            var result = from MB in _context.MagazineBranches
                         join M in _context.Magazines on MB.MagazineId equals M.Id
                         where MB.Id == Id
                         select new MagazineBranch(MB, M);
            return await result.FirstOrDefaultAsync();
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
            if (PageSize == 0)
            {
                validFilter.PageSize = await totalRecords;
            }
            var pagedData = await
                (
                    from MB in _context.MagazineBranches
                    join M in _context.Magazines on MB.MagazineId equals M.Id
                    orderby MB.Id ascending
                    select new MagazineBranch(MB,M)
                 )
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();


            var result = new GetAllWithPaging<MagazineBranch>(validFilter, pagedData, await totalRecords);
            return result;
        }
        public async Task<int> EmployeOfNumbers(int magazineBranchId)
        {
            return await _context.UserReferences.Where(x => x.MagazineBranchId == magazineBranchId).CountAsync();
        }
        public async Task<IEnumerable<UserReference>> GetUsersByBranchId(int branchId)
        {
            var branchUsers =  (
                                   from UR in _context.UserReferences
                                   join U in _context.Users on UR.UserId equals U.Id
                                   join MB in _context.MagazineBranches on UR.MagazineBranchId equals MB.Id into UR_MB
                                   from MB in UR_MB.DefaultIfEmpty()
                                   join P in _context.Positions on UR.PositionId equals P.Id into UR_P
                                   from P in UR_P.DefaultIfEmpty()
                                   where UR.MagazineBranchId == branchId
                                   select new UserReference(UR,U,MB,P)
                               ).ToListAsync();
            return await branchUsers;
        }
    }
}
