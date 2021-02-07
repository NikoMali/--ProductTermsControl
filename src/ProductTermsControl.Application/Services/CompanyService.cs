using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using ProductTermsControl.Insfrastructure.Filter;
using ProductTermsControl.Insfrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ProductTermsControl.Application.Services
{
    public interface ICompanyService : IDisposable
    {
        IEnumerable<Company> GetAll();
        string Update(Company company);
        string Delete(int Id);
        Company GetById(int Id);
        string Create(Company company);
        GetAllWithPaging<Company> GetAllForPaging(int PageNumber, int PageSize);
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
        public GetAllWithPaging<Company> GetAllForPaging(int PageNumber, int PageSize)
        {
            
            var validFilter = new PaginationFilter(PageNumber, PageSize);
            var pagedData = _companyRepository.GetAll() 
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            
            var totalRecords = _companyRepository.GetAll().Count();
            var result = new GetAllWithPaging<Company>(validFilter, pagedData, totalRecords);
            return result;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
