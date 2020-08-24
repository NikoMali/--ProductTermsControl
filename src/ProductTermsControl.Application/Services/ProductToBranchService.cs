using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.HelperModel;
using ProductTermsControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductTermsControl.Application.Services
{
    public interface IProductToBranchService : IDisposable
    {
        IEnumerable<ProductToBranch> GetAll();
        string Update(ProductToBranch productToBranch);
        string Delete(int Id);
        ProductToBranch GetById(int Id);
        string Create(IList<ProductToBranch> productToBranch);
        IQueryable<ProductWithTerm> GetAllProductByBranchId(int branchId);
        ProductToBranch GetProductViewTermByBranchId(int branchId, int productId);
        IQueryable<ProductWithTerm> GetAllProductByBranchIdAndResponsibleId(int branchId, int userId);
    }

    public class ProductToBranchService : IProductToBranchService
    {
        private readonly IProductToBranchRepository _productToBranchRepository;
        private readonly ICommonRepository _commonRepository;

        public ProductToBranchService(IProductToBranchRepository ProductToBranchRepository)
        {
            _productToBranchRepository = ProductToBranchRepository;
        }
        public IEnumerable<ProductToBranch> GetAll()
        {
            return _productToBranchRepository.GetAll();
        }

        public string Update(ProductToBranch productToBranch)
        {
            try
            {
                _productToBranchRepository.Update(productToBranch);
                _productToBranchRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
           
        }

        public ProductToBranch GetById(int Id) 
        {
            return _productToBranchRepository.GetById(Id);
        }

        public string Delete(int Id)
        {
            try
            {
                _productToBranchRepository.Remove(Id);
                _productToBranchRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
        }

        public string Create(IList<ProductToBranch> productToBranch)
        {
            _productToBranchRepository.AddRange(productToBranch);
            _productToBranchRepository.SaveChanges();
            return ResultStatus.SUCCESS;
        }
        public IQueryable<ProductWithTerm> GetAllProductByBranchId(int branchId)
        {
            return _commonRepository.GetAllProductByBranchId(branchId);
        }
        public ProductToBranch GetProductViewTermByBranchId(int branchId, int productId)
        {
            return _commonRepository.GetProductViewTermByBranchId(branchId, productId);
        }
        public IQueryable<ProductWithTerm> GetAllProductByBranchIdAndResponsibleId(int branchId, int userId)
        {
            return _commonRepository.GetAllProductByBranchIdAndResponsibleId(branchId, userId);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
