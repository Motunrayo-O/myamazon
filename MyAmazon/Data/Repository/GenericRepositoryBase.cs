using Microsoft.EntityFrameworkCore;
using MyAmazon.Data.Repository.Interfaces;
using System.Linq.Expressions;

namespace MyAmazon.Data.Repository;

 public abstract class GenericRepositoryBase<T> : IGenericRepositoryBase<T> where T : class
    {
        protected MyAmazonContext RepositoryContext { get; set; } 
        public GenericRepositoryBase(MyAmazonContext repositoryContext) 
        {
            RepositoryContext = repositoryContext; 
        }
        public IQueryable<T> FindAll() => RepositoryContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => 
            RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    }