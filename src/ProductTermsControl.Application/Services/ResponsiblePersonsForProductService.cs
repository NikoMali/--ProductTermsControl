﻿using Microsoft.EntityFrameworkCore;
using ProductTermsControl.Application.ApplicationDbContext;
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
    public interface IResponsiblePersonsForProductService
    {
        Task<IEnumerable<ResponsiblePersonsForProduct>> GetAll();
        Task<ResponsiblePersonsForProduct> Update(ResponsiblePersonsForProduct ResponsiblePersonsByProduct);
        Task<string> Delete(int Id);
        Task<ResponsiblePersonsForProduct> GetById(int Id);
        Task<string> Create(IList<ResponsiblePersonsForProduct> ResponsiblePersonsByProduct);
    }

    public class ResponsiblePersonsForProductService : IResponsiblePersonsForProductService
    {
       
        private readonly IApplicationDbContext _context;

        public ResponsiblePersonsForProductService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ResponsiblePersonsForProduct>> GetAll()
        {
            return await _context.ResponsiblePersonsForProducts.ToListAsync();
        }

        public async Task<ResponsiblePersonsForProduct> Update(ResponsiblePersonsForProduct ResponsiblePersonsByProduct)
        {
            try
            {
                _context.ResponsiblePersonsForProducts.Update(ResponsiblePersonsByProduct);
                await _context.SaveChangesAsync();
                return ResponsiblePersonsByProduct;
            }
            catch (Exception)
            {
                throw new AppException(ResultStatus.FAILED);
            }
           
        }

        public async Task<ResponsiblePersonsForProduct> GetById(int Id) 
        {
            return await _context.ResponsiblePersonsForProducts.FindAsync(Id);
        }

        public async Task<string> Delete(int Id)
        {
            try
            {
                _context.ResponsiblePersonsForProducts.Remove(await GetById(Id));
                await _context.SaveChangesAsync();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
        }

        public async Task<string> Create(IList<ResponsiblePersonsForProduct> ResponsiblePersonsByProduct)
        {
            for (int i = 0; i < ResponsiblePersonsByProduct.Count; i++)
            {
                bool IsExist;
                IsAlreadyAddUser(ResponsiblePersonsByProduct[i].UserId,out IsExist);
                if (!IsExist)
                {
                    await _context.ResponsiblePersonsForProducts.AddAsync(ResponsiblePersonsByProduct[i]);
                }
                else
                {
                    throw new AppException("Already add user >>" + (_context.Users.FindAsync(ResponsiblePersonsByProduct[i].UserId).Result.Username));
                }
            }
            await _context.SaveChangesAsync();
            return ResultStatus.SUCCESS;
        }

        

        private void IsAlreadyAddUser(int userId, out bool isExist)
        {
            //var k = _context.ResponsiblePersonsForProducts.SingleOrDefaultAsync(x => x.UserId == userId);
            isExist = _context.ResponsiblePersonsForProducts.SingleOrDefault(x => x.UserId == userId) != null;
        }
    }
}
