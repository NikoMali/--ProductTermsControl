using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Application.Services
{
    public interface IResponsiblePersonsForProductService : IDisposable
    {
        IEnumerable<ResponsiblePersonsForProduct> GetAll();
        string Update(ResponsiblePersonsForProduct ResponsiblePersonsByProduct);
        string Delete(int Id);
        ResponsiblePersonsForProduct GetById(int Id);
        string Create(IList<ResponsiblePersonsForProduct> ResponsiblePersonsByProduct);
    }

    public class ResponsiblePersonsForProductService : IResponsiblePersonsForProductService
    {
        private readonly IResponsiblePersonsForProductRepository _ResponsiblePersonsByProductRepository;

        public ResponsiblePersonsForProductService(IResponsiblePersonsForProductRepository ResponsiblePersonsByProductRepository)
        {
            _ResponsiblePersonsByProductRepository = ResponsiblePersonsByProductRepository;
        }
        public IEnumerable<ResponsiblePersonsForProduct> GetAll()
        {
            return _ResponsiblePersonsByProductRepository.GetAll();
        }

        public string Update(ResponsiblePersonsForProduct ResponsiblePersonsByProduct)
        {
            try
            {
                _ResponsiblePersonsByProductRepository.Update(ResponsiblePersonsByProduct);
                _ResponsiblePersonsByProductRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
           
        }

        public ResponsiblePersonsForProduct GetById(int Id) 
        {
            return _ResponsiblePersonsByProductRepository.GetById(Id);
        }

        public string Delete(int Id)
        {
            try
            {
                _ResponsiblePersonsByProductRepository.Remove(Id);
                _ResponsiblePersonsByProductRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
        }

        public string Create(IList<ResponsiblePersonsForProduct> ResponsiblePersonsByProduct)
        {
            _ResponsiblePersonsByProductRepository.AddRange(ResponsiblePersonsByProduct);
            _ResponsiblePersonsByProductRepository.SaveChanges();
            return ResultStatus.SUCCESS;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
