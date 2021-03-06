﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ProductTermsControl.Application.ApplicationDbContext;
using ProductTermsControl.Application.Filter;
using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Application.Localize;
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
        private readonly IStringLocalizer<Resource> _localizer;

        public ProductService(IApplicationDbContext context, IStringLocalizer<Resource> localizer)
        {
            _context = context;
            _localizer = localizer;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> Update(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                product.Company = await _context.Companys.FindAsync(product.CompanyId);
                return product;
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

        public async Task<Product> Create(Product product)
        {
            if (await _context.Products.AnyAsync(x=>x.IdentificationCode == product.IdentificationCode))
            {
                throw new AppException(_localizer["ProductIdentificationValid"]);
            }
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            product.Company = await _context.Companys.FindAsync(product.CompanyId);
            return product;
        }
        public async Task<GetAllWithPaging<Product>> GetAllForPaging(int PageNumber, int PageSize)
        {

            var validFilter = new PaginationFilter(PageNumber, PageSize);
            var totalRecords = _context.Products.CountAsync();
            if (PageSize == 0)
            {
                validFilter.PageSize = await totalRecords;
            }
            var pagedData = await
                (
                    from P in _context.Products.AsQueryable()
                    join C in _context.Companys.AsQueryable() on P.CompanyId equals C.Id
                    orderby P.Id descending
                    select new Product(P, C)
                )
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();

           // pagedData.OrderBy(x => x.Id);
            var result = new GetAllWithPaging<Product>(validFilter, pagedData, await totalRecords);
            return result;
        }

    }
}
