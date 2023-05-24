using Athena.Core.Common;

namespace Athena.DataAccess.Repositories;

public interface IRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey> where TPrimaryKey : struct
{
    
}