using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Interfaces;

namespace SchoolSystem.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SchoolDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(SchoolDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();
        public virtual async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public virtual Task UpdateAsync(T entity) { _dbSet.Update(entity); return Task.CompletedTask; }
        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null) _dbSet.Remove(entity);
        }
        public virtual async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
