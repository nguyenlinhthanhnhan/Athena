using Athena.Core.Common;
using Athena.DataAccess.Persistence;

namespace Athena.DataAccess.Repositories.Impl;

public class Repository<TEntity, TPrimaryKey> : BaseRepository<TEntity, TPrimaryKey>, IRepository<TEntity, TPrimaryKey>
    where TEntity : Entity<TPrimaryKey> where TPrimaryKey : struct
{
    public Repository(AthenaDbContext dbContext) : base(dbContext)
    {
    }
}