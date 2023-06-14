using System.Data;
using System.Reflection;
using Athena.Core.Common.Interfaces;
using Athena.Core.Entities;
using Athena.Shared.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Athena.DataAccess.Persistence;

public class AthenaDbContext : DbContext
{
    private readonly IDateTime _dateTime;
    private IDbContextTransaction? _currentTransaction;

    public AthenaDbContext(DbContextOptions<AthenaDbContext> options)
        : base(options)
    {
    }

    public AthenaDbContext(DbContextOptions<AthenaDbContext> options, IDateTime dateTime) : base(options)
    {
        _dateTime = dateTime;
    }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            return;
        }

        _currentTransaction =
            await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await SaveChangesAsync().ConfigureAwait(false);
            await _currentTransaction?.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).Select(x => x.Entity);
        foreach (var modifiedEntry in modifiedEntries)
        {
            if (modifiedEntry is IHasModificationTime entityHasModificationTime)
            {
                entityHasModificationTime.LastModificationTime = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).Select(x => x.Entity);
        foreach (var modifiedEntry in modifiedEntries)
        {
            if (modifiedEntry is IHasModificationTime entityHasModificationTime)
            {
                entityHasModificationTime.LastModificationTime = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    // Entities
    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PostCategory> PostCategories { get; set; }
    public DbSet<PostTag> PostTags { get; set; }
    public DbSet<PostMeta> PostMetas { get; set; }
    public DbSet<User> Users { get; set; }
}