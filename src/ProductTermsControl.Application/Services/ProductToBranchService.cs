using Microsoft.EntityFrameworkCore;
using ProductTermsControl.Application.ApplicationDbContext;
using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.HelperModel;
using ProductTermsControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTermsControl.Application.Services
{
    public interface IProductToBranchService
    {
        Task<IEnumerable<ProductToBranch>> GetAll();
        Task<ProductToBranch> Update(ProductToBranch productToBranch);
        Task<string> Delete(int Id);
        Task<ProductToBranch> GetById(int Id);
        Task<string> Create(IList<ProductToBranch> productToBranch);
        Task<IOrderedEnumerable<ProductWithTerm>> GetAllProductByBranchId(int branchId);
        Task<ProductToBranch> GetProductViewTermByBranchId(int branchId, int productId);
        Task<IOrderedEnumerable<ProductWithTerm>> GetAllProductByBranchIdAndResponsibleId(int branchId, int userId);
    }

    public class ProductToBranchService : IProductToBranchService
    {
        private readonly IApplicationDbContext _context;

        public ProductToBranchService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductToBranch>> GetAll()
        {
            return await _context.ProductToBranches.ToListAsync();
        }

        public async Task<ProductToBranch> Update(ProductToBranch productToBranch)
        {
            try
            {
                _context.ProductToBranches.Update(productToBranch);
                await _context.SaveChangesAsync();
                return productToBranch;
            }
            catch (Exception)
            {
                throw new AppException(ResultStatus.FAILED);
            }
           
        }

        public async Task<ProductToBranch> GetById(int Id) 
        {
            return await _context.ProductToBranches.FindAsync(Id);
        }

        public async Task<string> Delete(int Id)
        {
            try
            {
                _context.ProductToBranches.Remove(await GetById(Id));
                await _context.SaveChangesAsync();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
        }

        public async Task<string> Create(IList<ProductToBranch> productToBranch)
        {
            await _context.ProductToBranches.AddRangeAsync(productToBranch);
            await _context.SaveChangesAsync();
            return ResultStatus.SUCCESS;
        }
        public Task<IOrderedEnumerable<ProductWithTerm>> GetAllProductByBranchId(int branchId)
        {
            var GetProducts = _context.ProductToBranches.Join(
                                                             _context.Products,
                                                             PB => PB.ProductId,
                                                             P => P.Id,
                                                             (PB, P) => new ProductWithTerm(P, PB)
                                                         )
                                                         .AsEnumerable()
                                                         .Where(pb => pb.ProductToBranch.MagazineBranchId == branchId)
                                                         .OrderBy(r => r.WarningTermDateBegin);
            return Task.FromResult(GetProducts);
        }
        public async Task<ProductToBranch> GetProductViewTermByBranchId(int branchId, int productId)
        {
            var GetProducts =await _context.ProductToBranches.Join(
                                                            _context.Products,
                                                            PB => PB.ProductId,
                                                            P => P.Id,
                                                            (PB, P) => new { ProductToBranch = PB, Product = P }
                                                        )
                                                        .Where(pb => pb.ProductToBranch.MagazineBranchId == branchId && pb.ProductToBranch.ProductId == productId)
                                                        .FirstOrDefaultAsync();
            return  GetProducts.ProductToBranch;
        }
        public Task<IOrderedEnumerable<ProductWithTerm>> GetAllProductByBranchIdAndResponsibleId(int branchId, int userId)
        {
            var GetProducts = (from PB in _context.ProductToBranches
                              join P in _context.Products on PB.ProductId equals P.Id
                              join RPG in _context.ResponsiblePersonsGroups on PB.ResponsiblePersonsGroupId equals RPG.Id
                              join RPP in _context.ResponsiblePersonsForProducts on RPG.Id equals RPP.ResponsiblePersonsGroupId
                              where PB.MagazineBranchId == branchId && RPP.UserId == userId
                              select new ProductWithTerm(P,PB)).AsEnumerable();

            return Task.FromResult(GetProducts.OrderBy(list => list.WarningTermDateBegin));
        }
       
        
    }
}
