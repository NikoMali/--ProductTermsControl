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
        Task<IOrderedEnumerable<ProductWithTerm>> GetAllProductWithFiltres(int branchId, int userId);
        Task<ProductToBranch> GetProductViewTermByBranchId(int branchId, int productId);
        Task<IOrderedEnumerable<ProductWithTerm>> GetAllProductByBranchIdAndResponsibleId(int branchId, int userId);
        Task<IEnumerable<BranchProductStock>> OutOfStocks();
        Task<BranchProductStock> OutOfStockUpdate(BranchProductStock productToBranch);
        Task<string> OutOfStockRemove(int Id);
        Task<BranchProductStock> OutOfStockById(int Id);
        Task<BranchProductStock> OutOfStockCreate(BranchProductStock productToBranch);
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
            return 
                await (
                from PTB in _context.ProductToBranches
                join P in _context.Products on PTB.ProductId equals P.Id
                join MB in _context.MagazineBranches on PTB.MagazineBranchId equals MB.Id
                join RPG in _context.ResponsiblePersonsGroups on PTB.ResponsiblePersonsGroupId equals RPG.Id
                join BPS in _context.BranchProductStocks on PTB.Id equals BPS.ProductToBranchId into BPS_PB
                from BPS in BPS_PB.DefaultIfEmpty()
                where (BPS == null || BPS.IsOutOfStock != true)
                select new ProductToBranch(PTB,P,MB,RPG)
                ).ToListAsync();
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
            return await
                (
                 from PTB in _context.ProductToBranches
                 join P in _context.Products on PTB.ProductId equals P.Id
                 join MB in _context.MagazineBranches on PTB.MagazineBranchId equals MB.Id
                 join RPG in _context.ResponsiblePersonsGroups on PTB.ResponsiblePersonsGroupId equals RPG.Id
                 where PTB.Id == Id
                 select new ProductToBranch(PTB, P, MB, RPG)
                ).FirstOrDefaultAsync();
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
        public Task<IOrderedEnumerable<ProductWithTerm>> GetAllProductWithFiltres(int branchId, int userId)
        {
            /* var GetProducts = _context.ProductToBranches.Join(
                                                              _context.Products,
                                                              PB => PB.ProductId,
                                                              P => P.Id,
                                                              (PB, P) => new ProductWithTerm(P, PB)
                                                          )
                                                          .AsEnumerable()
                                                          .Where(pb => pb.ProductToBranch.MagazineBranchId == branchId)
                                                          .OrderBy(r => r.WarningTermDateBegin);*/
            var getProducts = new List<ProductWithTerm>();
            if (userId > 0)
            {
                getProducts.AddRange( 
                    (from PB in _context.ProductToBranches.Distinct()
                     join P in _context.Products on PB.ProductId equals P.Id
                    join BPS in _context.BranchProductStocks on PB.Id equals BPS.ProductToBranchId into BPS_PB
                    from BPS in BPS_PB.DefaultIfEmpty()
                    join RPG in _context.ResponsiblePersonsGroups on PB.ResponsiblePersonsGroupId equals RPG.Id
                    join RPFP in _context.ResponsiblePersonsForProducts on RPG.Id equals RPFP.ResponsiblePersonsGroupId
                    where 
                    (PB.MagazineBranchId == branchId && (BPS == null || BPS.IsOutOfStock != true) && RPFP.UserId == userId)
                     select new ProductWithTerm(P, PB)).AsEnumerable()
                    );

            }
            if (userId == 0)
            {
                getProducts.AddRange(
                    (from PB in _context.ProductToBranches
                     join P in _context.Products on PB.ProductId equals P.Id
                     join BPS in _context.BranchProductStocks on PB.Id equals BPS.ProductToBranchId into BPS_PB
                     from BPS in BPS_PB.DefaultIfEmpty()
                     where PB.MagazineBranchId == branchId && (BPS == null || BPS.IsOutOfStock != true)
                     select new ProductWithTerm(P, PB)).AsEnumerable()
                    );
            }

            
            return Task.FromResult(getProducts.OrderBy(r => r.WarningTermDateBegin));
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
                              join BPS in _context.BranchProductStocks on PB.Id equals BPS.ProductToBranchId into BPS_PB
                              from BPS in BPS_PB.DefaultIfEmpty()
                              where (BPS == null || BPS.IsOutOfStock != true)
                              where PB.MagazineBranchId == branchId && RPP.UserId == userId
                              select new ProductWithTerm(P,PB)).AsEnumerable();

            return Task.FromResult(GetProducts.OrderBy(list => list.WarningTermDateBegin));
        }

        public async Task<IEnumerable<BranchProductStock>> OutOfStocks()
        {
            var GetStocks = (from BPS in _context.BranchProductStocks
                            join PTB in _context.ProductToBranches on BPS.ProductToBranchId equals PTB.Id
                            join R in _context.ReasonForOutOfStocks on BPS.ReasonForOutOfStockId equals R.Id into BPS_R
                            from R in BPS_R.DefaultIfEmpty()
                            select new BranchProductStock(BPS, PTB, R)).ToListAsync();
            return await GetStocks;
        }
        public async Task<BranchProductStock> OutOfStockUpdate(BranchProductStock productToBranch)
        { 
            _context.BranchProductStocks.Update(productToBranch);
            await _context.SaveChangesAsync();
            productToBranch.ProductToBranch = await GetById(productToBranch.ProductToBranchId);
            return productToBranch;
        }
        public async Task<string> OutOfStockRemove(int Id)
        {
            _context.BranchProductStocks.Remove(await _context.BranchProductStocks.FindAsync(Id));
            await _context.SaveChangesAsync();
            return ResultStatus.SUCCESS;
        }
        public async Task<BranchProductStock> OutOfStockById(int Id)
        {
            var GetStocks = (from BPS in _context.BranchProductStocks
                             join PTB in _context.ProductToBranches on BPS.ProductToBranchId equals PTB.Id
                             join R in _context.ReasonForOutOfStocks on BPS.ReasonForOutOfStockId equals R.Id
                             where BPS.Id == Id
                             select new BranchProductStock(BPS, PTB,R)).FirstOrDefaultAsync();
            return await GetStocks;
        }
        public async Task<BranchProductStock> OutOfStockCreate(BranchProductStock productToBranch)
        {
            
            await _context.BranchProductStocks.AddAsync(productToBranch);
            await _context.SaveChangesAsync();
            productToBranch.ProductToBranch =await GetById(productToBranch.ProductToBranchId);
            return productToBranch;
        }

    }
}
