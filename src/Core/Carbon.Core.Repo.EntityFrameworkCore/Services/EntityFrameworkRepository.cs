using Carbon.Core.Domain.Models.Base;
using Carbon.Core.Repo.Interfaces;

using LinqSpecs;

using Microsoft.EntityFrameworkCore;

namespace Carbon.Core.Repo.EntityFrameworkCore.Services;

/// <inheritdoc/>
/// <summary>
/// Базовая реализация <see cref="IRepository{TEntity, TId}"/> для Entity Framework Core
/// </summary>
public abstract class EntityFrameworkRepository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : IEquatable<TId>
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected EntityFrameworkRepository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual Task<TId> CreateAsync(TEntity entity, CancellationToken token = default)
    {
        DbSet.Add(entity);
        return Task.FromResult(entity.Id);
    }

    public virtual Task DeleteAsync(TEntity entity, CancellationToken token = default)
    {
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual IAsyncEnumerable<TEntity> GetManyAsync(Specification<TEntity>? specification = null, Range range = default)
    {
        var query = DbSet.AsNoTracking()
            .Where(specification ?? new TrueSpecification<TEntity>());

        if (!range.Equals(default) && !range.Equals(Range.All))
        {
            CheckRange(range);
            query = query.Skip(range.Start.Value).Take(range.End.Value - range.Start.Value);
        }

        return query.AsAsyncEnumerable();
    }

    public virtual IAsyncEnumerable<TEntity> GetManyByIdsAsync(IEnumerable<TId> ids)
    {
        return DbSet.AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .AsAsyncEnumerable();
    }

    public virtual Task<TEntity?> GetOneAsync(Specification<TEntity>? specification = null, CancellationToken token = default)
    {
        return DbSet.AsNoTracking()
            .Where(specification ?? new TrueSpecification<TEntity>())
            .FirstOrDefaultAsync(token);
    }

    public virtual Task<TEntity?> GetOneByIdAsync(TId id, CancellationToken token = default)
    {
        return DbSet.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), token);
    }

    public virtual Task UpdateAsync(TEntity entity, CancellationToken token = default)
    {
        DbSet.Update(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Проверка на валидность Range
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    protected void CheckRange(Range range)
    {
        if (range.Start.IsFromEnd || range.End.IsFromEnd)
            throw new NotSupportedException("Range with start or end from end is not supported");
    }
}