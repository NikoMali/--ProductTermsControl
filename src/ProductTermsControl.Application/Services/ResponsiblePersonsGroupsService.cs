using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Application.Services
{
    public interface IResponsiblePersonsGroupService : IDisposable
    {
        IEnumerable<ResponsiblePersonsGroup> GetAll();
        string Update(ResponsiblePersonsGroup responsiblePersonsGroup);
        string Delete(int Id);
        ResponsiblePersonsGroup GetById(int Id);
        string Create(ResponsiblePersonsGroup responsiblePersonsGroup);
    }

    public class ResponsiblePersonsGroupService : IResponsiblePersonsGroupService
    {
        private readonly IResponsiblePersonsGroupRepository _responsiblePersonsGroupRepository;

        public ResponsiblePersonsGroupService(IResponsiblePersonsGroupRepository responsiblePersonsGroupRepository)
        {
            _responsiblePersonsGroupRepository = responsiblePersonsGroupRepository;
        }
        public IEnumerable<ResponsiblePersonsGroup> GetAll()
        {
            return _responsiblePersonsGroupRepository.GetAll();
        }

        public string Update(ResponsiblePersonsGroup ResponsiblePersonsGroup)
        {
            try
            {
                _responsiblePersonsGroupRepository.Update(ResponsiblePersonsGroup);
                _responsiblePersonsGroupRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
           
        }

        public ResponsiblePersonsGroup GetById(int Id) 
        {
            return _responsiblePersonsGroupRepository.GetById(Id);
        }

        public string Delete(int Id)
        {
            try
            {
                _responsiblePersonsGroupRepository.Remove(Id);
                _responsiblePersonsGroupRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
        }

        public string Create(ResponsiblePersonsGroup ResponsiblePersonsGroup)
        {
            _responsiblePersonsGroupRepository.Add(ResponsiblePersonsGroup);
            _responsiblePersonsGroupRepository.SaveChanges();
            return ResultStatus.SUCCESS;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
