using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Identity.Domain.RepositoryAbstraction
{
    public interface IBaseRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> Get(int? id);
        DbSet<TEntity> Table { get; }
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
    }
}
