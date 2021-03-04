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
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Update(Product product);
        Task<string> Delete(int Id);
        Task<Product> GetById(int Id);
        Task<Product> Create(Product product);
        Task<GetAllWithPaging<Product>> GetAllForPaging(int PageNumber, int PageSize);
    }

    public class ProductService : IProductService
    {
        private readonly IApplicationDbContext _context;

        public ProductService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> Update(Product Product)
        {
            try
            {
                _context.Products.Update(Product);
                await _context.SaveChangesAsync();
                return Product;
            }
            catch (Exception)
            {
                throw new AppException(ResultStatus.FAILED);
            }
           
        }

        public async Task<Product> GetById(int Id) 
        {
            var result = from P in _context.Products
                         join C in _context.Companys on P.CompanyId equals C.Id
                         where P.Id == Id
                         select new Product(P, C);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<string> Delete(int Id)
        {
            try
            {
                _context.Products.Remove(await _context.Products.FindAsync(Id));
                await _context.SaveChangesAsync();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                throw new AppException(ResultStatus.FAILED);
            }
        }

        public async Task<Product> Create(Product Product)
        {
            await _context.Products.AddAsync(Product);
            await _context.SaveChangesAsync();
            return Product;
        }
        public async Task<GetAllWithPaging<Product>> GetAllForPaging(int PageNumber, int PageSize)
        {

            var validFilter = new PaginationFilter(PageNumber, PageSize);
            var totalRecords = _context.Products.CountAsync();
            var pagedData = await 
                (
                    from P in _context.Products
                    join C in _context.Companys on P.CompanyId equals C.Id
                    select new Product(P,C)
                )
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();


            var result = new GetAllWithPaging<Product>(validFilter, pagedData, await totalRecords);
            return result;
        }

    }
}
