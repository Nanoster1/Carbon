namespace Carbon.Core.Repo.Interfaces;

/// <summary>
/// Интерфейс, реализующий сохранение изменений в репозиториях
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Сохраняет изменения во всех репозиториях
    /// </summary>
    Task SaveChangesAsync();
}