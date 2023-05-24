using System.Linq.Expressions;
using Athena.Core.Common;

namespace Athena.DataAccess.Repositories;

public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey> where TPrimaryKey : struct
{
    Task<TEntity?> FirstOrDefaultAsync(TPrimaryKey id);

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> InsertAsync(TEntity entity);

    Task BulkInsertAsync(IEnumerable<TEntity> entities);
    
    Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<TEntity> DeleteAsync(TEntity entity);
}