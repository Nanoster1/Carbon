using Carbon.Core.Domain.Models.Base;

using LinqSpecs;

namespace Carbon.Core.Repo.Interfaces;

/// <summary>
/// Базовый интерфейс репозитория
/// </summary>
/// <typeparam name="TEntity">
/// Тип сущности, которую репозиторий должен хранить
/// </typeparam>
/// <typeparam name="TId">
/// Тип идентификатора сущности
/// </typeparam>
public interface IRepository<TEntity, TId> : IRepository
    where TEntity : Entity<TId>
    where TId : IEquatable<TId>
{
    /// <summary>
    /// Создаёт сущность в репозитории и создаёт ей идентификатор, если он не был задан
    /// </summary>
    /// <param name="entity">
    /// Сущность, которую нужно добавить в репозиторий
    /// </param>
    /// <param name="token">
    /// Токен отмены операции
    /// </param>
    /// <returns>
    /// Идентификатор сущности
    /// </returns>
    Task<TId> CreateAsync(TEntity entity, CancellationToken token = default);
    /// <summary>
    /// Берёт сущность из репозитория по её идентификатору
    /// </summary>
    /// <param name="id">
    /// Идентификатор сущности
    /// </param>
    /// <param name="token">
    /// Токен отмены операции
    /// </param>
    /// <returns>
    /// Сущность, если она была найдена, иначе null
    /// </returns>
    Task<TEntity?> GetOneByIdAsync(TId id, CancellationToken token = default);
    /// <summary>
    /// Берёт сущности из репозитория по их идентификаторам
    /// </summary>
    /// <param name="ids">
    /// Идентификаторы сущностей
    /// </param>
    /// <param name="token">
    /// Токен отмены операции
    /// </param>
    /// <returns>
    /// Сущности, если они были найдены, иначе пустой список
    /// </returns>
    IAsyncEnumerable<TEntity> GetManyByIdsAsync(IEnumerable<TId> ids);
    /// <summary>
    /// Берёт сущность из репозитория по спецификации
    /// </summary>
    /// <param name="specification">
    /// Спецификация, по которой будет производиться поиск
    /// </param>
    /// <param name="token">
    /// Токен отмены операции
    /// </param>
    /// <returns>
    /// Сущность, если она была найдена, иначе null
    /// </returns>
    Task<TEntity?> GetOneAsync(Specification<TEntity>? specification = null, CancellationToken token = default);
    /// <summary>
    /// Берёт сущности из репозитория по спецификации
    /// </summary>
    /// <param name="specification">
    /// Спецификация, по которой будет производиться поиск
    /// </param>
    /// <param name="token">
    /// Токен отмены операции
    /// </param>
    /// <returns>
    /// Сущности, если они были найдены, иначе пустой список
    /// </returns>
    IAsyncEnumerable<TEntity> GetManyAsync(Specification<TEntity>? specification = null, Range range = default);
    /// <summary>
    /// Обновляет сущность в репозитории
    /// </summary>
    /// <param name="entity">
    /// Сущность, которую нужно обновить
    /// </param>
    /// <param name="token">
    /// Токен отмены операции
    /// </param>
    Task UpdateAsync(TEntity entity, CancellationToken token = default);
    /// <summary>
    /// Удаляет сущность из репозитория
    /// </summary>
    /// <param name="entity">
    /// Сущность, которую нужно удалить
    /// </param>
    /// <param name="token">
    /// Токен отмены операции
    /// </param>
    Task DeleteAsync(TEntity entity, CancellationToken token = default);
}

/// <summary>
/// Маркерный интерфейс для репозиториев, наследовать от него не нужно <br/>
/// Используется для регистрации в DI-контейнере <br/>
/// Вместо него нужно реализовывать <see cref="IRepository{TEntity, TId}"/>
/// </summary>
public interface IRepository
{
}