using BoxTicketApi.DAL.Contexts;
using BoxTicketApi.DAL.Entities.Base;
using BoxTicketApi.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxTicketApi.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly BoxTicketContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(BoxTicketContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            await SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);

            await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity is not null)
            {
                _dbSet.Remove(entity);

                await SaveChangesAsync();
            }
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
