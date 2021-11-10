using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.RepositoryAbstraction
{
    public interface IBaseRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> Get(int? id);
        DbSet<TEntity> Table { get; }
        Task<int> CreateAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
    }
}
