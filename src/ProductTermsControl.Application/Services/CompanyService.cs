using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Application.Services
{
    public interface ICompanyService : IDisposable
    {
        IEnumerable<Company> GetAll();
        string Update(Company company);
        string Delete(int Id);
        Company GetById(int Id);
        string Create(Company company);
    }

    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public IEnumerable<Company> GetAll()
        {
            return _companyRepository.GetAll();
        }

        public string Update(Company Company)
        {
            try
            {
                _companyRepository.Update(Company);
                _companyRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
           
        }

        public Company GetById(int Id) 
        {
            return _companyRepository.GetById(Id);
        }

        public string Delete(int Id)
        {
            try
            {
                _companyRepository.Remove(Id);
                _companyRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
        }

        public string Create(Company Company)
        {
            _companyRepository.Add(Company);
            _companyRepository.SaveChanges();
            return ResultStatus.SUCCESS;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
