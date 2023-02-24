using Carbon.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Carbon.Migrations;

public class Factory : IDesignTimeDbContextFactory<CarbonDbContext>
{
    public CarbonDbContext CreateDbContext(string[] args)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .AddUserSecrets<Factory>()
            .AddEnvironmentVariables();

        var configuration = configurationBuilder.Build();
        var connectionString = configuration.GetConnectionString(CarbonDbContext.ConnectionStringName);
        var optionsBuilder = new DbContextOptionsBuilder<CarbonDbContext>();
        CarbonDbContext.Configure(optionsBuilder, configuration, typeof(Factory).Assembly);

        return new CarbonDbContext(optionsBuilder.Options);
    }
}