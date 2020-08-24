using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Application.Services
{
    public interface IProductService : IDisposable
    {
        IEnumerable<Product> GetAll();
        string Update(Product product);
        string Delete(int Id);
        Product GetById(int Id);
        string Create(Product product);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public string Update(Product Product)
        {
            try
            {
                _productRepository.Update(Product);
                _productRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
           
        }

        public Product GetById(int Id) 
        {
            return _productRepository.GetById(Id);
        }

        public string Delete(int Id)
        {
            try
            {
                _productRepository.Remove(Id);
                _productRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
        }

        public string Create(Product Product)
        {
            _productRepository.Add(Product);
            _productRepository.SaveChanges();
            return ResultStatus.SUCCESS;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
