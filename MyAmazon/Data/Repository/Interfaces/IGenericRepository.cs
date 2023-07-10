using System.Linq.Expressions;

namespace MyAmazon.Data.Repository.Interfaces;
public interface IGenericRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    Task<T> GetById(Guid id);
    Task Create(T entity);
    void Update(T entity);
    Task Delete(Guid id);
    Task Save();
}