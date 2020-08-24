using ProductTermsControl.Application.Helpers;
using ProductTermsControl.Domain.Entities;
using ProductTermsControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductTermsControl.Application.Services
{
    public interface IMagazineBranchService : IDisposable
    {
        IEnumerable<MagazineBranch> GetAll();
        string Update(MagazineBranch magazineBranch);
        string Delete(int Id);
        MagazineBranch GetById(int Id);
        string Create(MagazineBranch magazineBranch);
        IEnumerable<MagazineBranch> GetAllByMagazineId(int magazineId);
    }

    public class MagazineBranchService : IMagazineBranchService
    {
        private readonly IMagazineBranchRepository _magazineBranchRepository;

        public MagazineBranchService(IMagazineBranchRepository magazineBranchRepository)
        {
            _magazineBranchRepository = magazineBranchRepository;
        }
        public IEnumerable<MagazineBranch> GetAll()
        {
            return _magazineBranchRepository.GetAll();
        }

        public string Update(MagazineBranch magazineBranch)
        {
            try
            {
                _magazineBranchRepository.Update(magazineBranch);
                _magazineBranchRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
           
        }

        public MagazineBranch GetById(int Id) 
        {
            return _magazineBranchRepository.GetById(Id);
        }

        public string Delete(int Id)
        {
            try
            {
                _magazineBranchRepository.Remove(Id);
                _magazineBranchRepository.SaveChanges();
                return ResultStatus.SUCCESS;
            }
            catch (Exception)
            {
                return ResultStatus.FAILED;
            }
        }

        public string Create(MagazineBranch MagazineBranch)
        {
            _magazineBranchRepository.Add(MagazineBranch);
            _magazineBranchRepository.SaveChanges();
            return ResultStatus.SUCCESS;
        }

        public IEnumerable<MagazineBranch> GetAllByMagazineId(int magazineId)
        {
            return _magazineBranchRepository.GetAllByMagazineId(magazineId);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
