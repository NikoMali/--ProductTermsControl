using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Application.Services
{
    public interface IMagazineService : IDisposable
    {
        IEnumerable<Magazine> GetAll();
        string Update(Magazine magazine);
        string Delete(int Id);
        Magazine GetById(int Id);
        string Create(Magazine magazine);
    }

    public class MagazineService : IMagazineService
    {
        private readonly IMagazineRepository _magazineRepository;

        public MagazineService(IMagazineRepository magazineRepository)
        {
            _magazineRepository = magazineRepository;
        }
        public IEnumerable<Magazine> GetAll()
        {
            return _magazineRepository.GetAll();
        }

        public string Update(Magazine magazine)
        {
            try
            {
                _magazineRepository.Update(magazine);
                _magazineRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
           
        }

        public Magazine GetById(int Id) 
        {
            return _magazineRepository.GetById(Id);
        }

        public string Delete(int Id)
        {
            try
            {
                _magazineRepository.Remove(Id);
                _magazineRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
        }

        public string Create(Magazine magazine)
        {
            _magazineRepository.Add(magazine);
            _magazineRepository.SaveChanges();
            return ResultStatus.SUCCESS;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
