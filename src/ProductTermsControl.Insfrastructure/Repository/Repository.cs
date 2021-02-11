using Microsoft.EntityFrameworkCore;
using ProductTermsControl.Domain.Interfaces;
using ProductTermsControl.Application.Filter;
using ProductTermsControl.Insfrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductTermsControl.Insfrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DataContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }
        public virtual void AddRange(IList<TEntity> obj)
        {
            DbSet.AddRange(obj);
        }

        public virtual TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }
        /*public virtual GetAllWithPaging<TEntity> GetAllFilter(int PageNumber, int PageSize)
        {
            var validFilter = new PaginationFilter(PageNumber, PageSize);
            var pagedData = DbSet
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();


            var totalRecords = DbSet.Count();
            var result = new GetAllWithPaging<TEntity>(validFilter, pagedData, totalRecords);
            return result;

        }*/

        public virtual IQueryable<TEntity> GetAll(ISpecification<TEntity> spec)
        {
            return ApplySpecification(spec);
        }

        public virtual IQueryable<TEntity> GetAllSoftDeleted()
        {
            return DbSet.IgnoreQueryFilters()
                .Where(e => EF.Property<bool>(e, "IsDeleted") == true);
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(DbSet.AsQueryable(), spec);
        }

        IQueryable<TEntity> IRepository<TEntity>.GetAll()
        {
            return DbSet;
        }
       

        IQueryable<TEntity> IRepository<TEntity>.GetAll(ISpecification<TEntity> spec)
        {
            throw new NotImplementedException();
        }

        IQueryable<TEntity> IRepository<TEntity>.GetAllSoftDeleted()
        {
            throw new NotImplementedException();
        }
    }
}
