using System.Linq.Expressions;
using Athena.Core.Common;
using Athena.Core.Exceptions;
using Athena.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Athena.DataAccess.Repositories.Impl;

public abstract class BaseRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey>
    where TEntity : Entity<TPrimaryKey> where TPrimaryKey : struct
{
    protected readonly AthenaDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(AthenaDbContext dbContext)
    {
        Context = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> FirstOrDefaultAsync(TPrimaryKey id)
    {
        var entity = await DbSet.FindAsync(id);

        if (entity == null) throw new ResourceNotFoundException(typeof(TEntity));

        return entity;
    }

    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = DbSet.FirstOrDefaultAsync(predicate);

        if (entity == null) throw new ResourceNotFoundException(typeof(TEntity));

        return entity;
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate) =>
        await DbSet.Where(predicate).ToListAsync();

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var addedEntity = (await DbSet.AddAsync(entity)).Entity;
        await Context.SaveChangesAsync();

        return addedEntity;
    }

    public async Task BulkInsertAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
        await Context.SaveChangesAsync();
    }

    public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
    {
        var addedEntity = (await DbSet.AddAsync(entity)).Entity;
        await Context.SaveChangesAsync();

        return addedEntity.Id;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var removedEntity = DbSet.Remove(entity).Entity;
        await Context.SaveChangesAsync();

        return removedEntity;
    }
}