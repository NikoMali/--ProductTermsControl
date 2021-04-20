using Microsoft.EntityFrameworkCore;
using ProductTermsControl.Application.ApplicationDbContext;
using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.HelperModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ProductTermsControl.Application.Services
{
    public interface IResponsiblePersonsGroupService
    {
        Task<IEnumerable<ResponsiblePersonsGroup>> GetAll();
        Task<ResponsiblePersonsGroup> Update(ResponsiblePersonsGroup responsiblePersonsGroup);
        Task<string> Delete(int Id);
        Task<ResponsiblePersonsGroup> GetById(int Id);
        Task<ResponsiblePersonsGroup> Create(ResponsiblePersonsGroup responsiblePersonsGroup);
        Task<List<SectionWithUsersAndProducts>> SectionWithUsersAndProducts();
    }

    public class ResponsiblePersonsGroupService : IResponsiblePersonsGroupService
    {
        private readonly IApplicationDbContext _context;

        public ResponsiblePersonsGroupService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ResponsiblePersonsGroup>> GetAll()
        {
            return await _context.ResponsiblePersonsGroups.ToListAsync();
        }

        public async Task<ResponsiblePersonsGroup> Update(ResponsiblePersonsGroup responsiblePersonsGroup)
        {
            
                _context.ResponsiblePersonsGroups.Update(responsiblePersonsGroup);
                await _context.SaveChangesAsync();
                return responsiblePersonsGroup;
            
           
        }

        public async Task<ResponsiblePersonsGroup> GetById(int Id) 
        {
            return await _context.ResponsiblePersonsGroups.FindAsync(Id);
        }

        public async Task<string> Delete(int Id)
        {
            try
            {
                _context.ResponsiblePersonsGroups.Remove(await _context.ResponsiblePersonsGroups.FindAsync(Id));
                await _context.SaveChangesAsync();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
        }

        public async Task<ResponsiblePersonsGroup> Create(ResponsiblePersonsGroup responsiblePersonsGroup)
        {
            await _context.ResponsiblePersonsGroups.AddAsync(responsiblePersonsGroup);
            await _context.SaveChangesAsync();
            return responsiblePersonsGroup;
        }

        public async Task<List<SectionWithUsersAndProducts>> SectionWithUsersAndProducts()
        {
            var result = new List<SectionWithUsersAndProducts>();
            var getSections = await _context.ResponsiblePersonsGroups.ToListAsync();

            for (int i = 0; i < getSections.Count; i++)
            {
                
                var getUsers = await (from RPG in _context.ResponsiblePersonsGroups
                                      join RPFP in _context.ResponsiblePersonsForProducts on RPG.Id equals RPFP.ResponsiblePersonsGroupId
                                      join U in _context.Users on RPFP.UserId equals U.Id
                                      where RPG.Id == getSections[i].Id
                                      select new User(U)).ToListAsync();

                var getProducts = await (from RPG in _context.ResponsiblePersonsGroups
                                         join PB in _context.ProductToBranches on RPG.Id equals PB.ResponsiblePersonsGroupId
                                         join P in _context.Products on PB.ProductId equals P.Id
                                         join C in _context.Companys on P.CompanyId equals C.Id
                                         where RPG.Id == getSections[i].Id
                                         select new Product(P, C)).ToListAsync();

                result.Add(new SectionWithUsersAndProducts(getSections[i], getUsers, getProducts));
            }
            return result;
        }
    }
}
