using Inflow.Core.Data.Options;
using Inflow.Core.Data.Schema;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Execution;
using InflowDataQuery = Inflow.Core.Data.Query;

namespace Inflow.Core.Data.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddScopedInflowDataQuery(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IDataQueryable, InflowDataQuery>(serviceProvider =>
        {
            var databaseProvider = serviceProvider.GetRequiredService<QueryFactory>(); 
            return new InflowDataQuery(databaseProvider);
        });
    }

    public static IServiceCollection AddScopedDatabaseProvider(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<QueryFactory>(serviceProvider =>
        {
            var sqlOptions = serviceProvider.GetRequiredService<BaseSqlOptions>();
            return new QueryFactory(sqlOptions.DbConnection, sqlOptions.Compiler);
        });
    }

    public static IServiceCollection AddScopedSqlOptions(this IServiceCollection serviceCollection,
        string sqlOptionsName, string dbConnectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sqlOptionsName);
        ArgumentException.ThrowIfNullOrWhiteSpace(dbConnectionString);
        return serviceCollection.AddScoped<BaseSqlOptions>(_ =>
        {
            switch (sqlOptionsName)
            {
                case nameof(SqlServerOptions):
                    return new SqlServerOptions { DbConnection = { ConnectionString = dbConnectionString } };
                case nameof(PostgreSqlOptions):
                    return new PostgreSqlOptions { DbConnection = { ConnectionString = dbConnectionString } };
                case nameof(MySqlOptions):
                    return new MySqlOptions { DbConnection = { ConnectionString = dbConnectionString } };
                default:
                    var exceptionMessage = 
                        string.Format(Resources.SqlOptionsAreNotImplemented, sqlOptionsName);
                    throw new NotImplementedException(exceptionMessage);
            }
        });
    }

    public static IServiceCollection AddSingletonSqlSchema(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<ISchema>(serviceProvider =>
        {
            var databaseProvider = serviceProvider.GetRequiredService<QueryFactory>();
            return new Schema.Schema(databaseProvider);
        });
    }
}