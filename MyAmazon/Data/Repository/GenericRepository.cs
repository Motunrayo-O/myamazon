using Microsoft.EntityFrameworkCore;
using MyAmazon.Data.Repository.Interfaces;

namespace MyAmazon.Data.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly MyAmazonContext _context;
    private readonly DbSet<T> _entities;

    public GenericRepository(MyAmazonContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public IEnumerable<T> GetAll() =>
        _entities.ToList();

    public async Task<T> GetById(Guid id) =>
         await _entities.FindAsync(id);

    public async Task Create(T entity) =>
        await _context.AddAsync(entity);

    public void Update(T entity)
    {
        _entities.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task Delete(Guid id)
    {
        T existing = await _entities.FindAsync(id);
        _entities.Remove(existing);
    }

    public async Task Save() =>
      await _context.SaveChangesAsync();
}