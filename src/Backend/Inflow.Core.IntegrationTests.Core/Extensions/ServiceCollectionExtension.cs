using Inflow.Core.Data.Options;
using Inflow.Core.IntegrationTests.Core.Database;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Execution;

namespace Inflow.Core.IntegrationTests.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTransientTestDatabasePreparer(this IServiceCollection serviceCollection, 
        string sqlOptionsName, int timeout)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sqlOptionsName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(timeout);
        switch (sqlOptionsName)
        {
            case nameof(SqlServerOptions):
                return serviceCollection.AddTransient<ITestDatabasePreparer, MsSqlTestDatabasePreparer>(
                    _ => new MsSqlTestDatabasePreparer(sqlOptionsName, timeout));
            case nameof(PostgreSqlOptions):
                return serviceCollection.AddTransient<ITestDatabasePreparer, PostgreSqlTestDatabasePreparer>(
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