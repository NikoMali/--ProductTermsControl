using Microsoft.EntityFrameworkCore;
using ProductTermsControl.Application.ApplicationDbContext;
using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductTermsControl.Application.Services
{
    public interface IResponsiblePersonsGroupService
    {
        Task<IEnumerable<ResponsiblePersonsGroup>> GetAll();
        Task<ResponsiblePersonsGroup> Update(ResponsiblePersonsGroup responsiblePersonsGroup);
        Task<string> Delete(int Id);
        Task<ResponsiblePersonsGroup> GetById(int Id);
        Task<ResponsiblePersonsGroup> Create(ResponsiblePersonsGroup responsiblePersonsGroup);
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

        
    }
}
