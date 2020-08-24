using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductTermsControl.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        void AddRange(IList<TEntity> obj);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(ISpecification<TEntity> spec);
        IQueryable<TEntity> GetAllSoftDeleted();
        void Update(TEntity obj);
        void Remove(int id);
        int SaveChanges();
    }
}
