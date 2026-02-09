using Inflow.Core.Data.Options;
using Inflow.Core.IntegrationTests.Core.Database;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Execution;

namespace Inflow.Core.IntegrationTests.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSingletonTestDatabasePreparer(this IServiceCollection serviceCollection, 
        string sqlOptionsName, int timeout)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sqlOptionsName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(timeout);
        switch (sqlOptionsName)
        {
            case nameof(SqlServerOptions):
                return serviceCollection.AddSingleton<ITestDatabasePreparer, MsSqlTestDatabasePreparer>(
                    serviceProvider =>
                    {
                        var query = serviceProvider.GetRequiredService<QueryFactory>();
                        return new MsSqlTestDatabasePreparer(query, sqlOptionsName, timeout);
                    });
            case nameof(PostgreSqlOptions):
                return serviceCollection.AddSingleton<ITestDatabasePreparer, PostgreSqlTestDatabasePreparer>(
                    serviceProvider =>
                    {
                        var query = serviceProvider.GetRequiredService<QueryFactory>();
                        return new PostgreSqlTestDatabasePreparer(query, sqlOptionsName, timeout);
                    });
            default:
                throw new NotImplementedException(sqlOptionsName);
        }
    }
}