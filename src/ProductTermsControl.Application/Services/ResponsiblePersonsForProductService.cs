using Microsoft.EntityFrameworkCore;
using ProductTermsControl.Application.ApplicationDbContext;
using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return await (
                    from RPFP in _context.ResponsiblePersonsForProducts.AsNoTracking()
                    join RPG in _context.ResponsiblePersonsGroups on RPFP.ResponsiblePersonsGroupId equals RPG.Id
                    join U in _context.Users on RPFP.UserId equals U.Id
                    select new ResponsiblePersonsForProduct(RPFP,RPG,U)
                ).ToListAsync();
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
            return await (
                 from RPFP in _context.ResponsiblePersonsForProducts.AsNoTracking()
                 join RPG in _context.ResponsiblePersonsGroups on RPFP.ResponsiblePersonsGroupId equals RPG.Id
                 join U in _context.Users on RPFP.UserId equals U.Id
                 where RPFP.Id == Id
                 select new ResponsiblePersonsForProduct(RPFP, RPG, U)
                ).FirstOrDefaultAsync();
        }

        public async Task<string> Delete(int Id)
        {
            var getResponsible = await GetById(Id);
            _context.ResponsiblePersonsForProducts.Remove(getResponsible);
            await _context.SaveChangesAsync();
            return ResultStatus.SUCCESS;
            
        }

        public async Task<string> Create(IList<ResponsiblePersonsForProduct> ResponsiblePersonsByProduct)
        {
            for (int i = 0; i < ResponsiblePersonsByProduct.Count; i++)
            {
                bool IsExist;
                IsAlreadyAddUser(ResponsiblePersonsByProduct[i].UserId,out IsExist);
                if (!IsExist)
                {
                    ResponsiblePersonsByProduct[i].RegisterDate = DateTime.Now;
                    await _context.ResponsiblePersonsForProducts.AddAsync(ResponsiblePersonsByProduct[i]);
                }
                else
                {
                    throw new AppException("Already add user >> " + (_context.Users.FindAsync(ResponsiblePersonsByProduct[i].UserId).Result.Username));
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
