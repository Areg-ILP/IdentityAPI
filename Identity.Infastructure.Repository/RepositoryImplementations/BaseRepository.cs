using Identity.Domain.Entities;
using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Identity.Infastructure.RepositoryImplementations
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
        protected readonly IdentityDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public BaseRepository(IdentityDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public virtual Task<TEntity> Get(int? id)
        {
            return _entities.FirstOrDefaultAsync(e => e.Id == id);
        }

        public DbSet<TEntity> Table
        {
            get
            {
                return _entities;
            }
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            if (entity == null)
                throw new NullReferenceException();
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();

            var res = entity.Id;         

        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new NullReferenceException();
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new NullReferenceException();
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
