using Carbon.Core.Repo.Interfaces;

namespace Carbon.Infrastructure.Persistance;

/// <summary>
/// Реализация UnitOfWork
/// </summary>
public sealed class UnitOfWork : IUnitOfWork
{
    private readonly CarbonDbContext _context;

    public UnitOfWork(CarbonDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}