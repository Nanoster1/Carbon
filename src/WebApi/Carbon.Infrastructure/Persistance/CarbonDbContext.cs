using System.Reflection;

using Carbon.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Carbon.Infrastructure.Persistance;

/// <summary>
/// Контекст базы данных
/// </summary>
public sealed class CarbonDbContext : DbContext
{
    /// <summary>
    /// Имя строки подключения в конфигурации
    /// </summary>
    public const string ConnectionStringName = "CarbonDb";

    /// <summary>
    /// Настройка контекста базы данных
    /// </summary>
    public static void Configure(
        DbContextOptionsBuilder optionsBuilder,
        IConfiguration configuration,
        Assembly? migrationsAssembly = null)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(ConnectionStringName),
            builder =>
            {
                if (migrationsAssembly != null) builder.MigrationsAssembly(migrationsAssembly.GetName().ToString());
            });
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    public CarbonDbContext(DbContextOptions<CarbonDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarbonDbContext).Assembly);
    }
}