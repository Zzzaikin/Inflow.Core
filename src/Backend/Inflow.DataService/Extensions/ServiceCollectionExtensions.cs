using Inflow.Data;
using Inflow.Data.Schema;
using Inflow.Data.Options;
using SqlKata.Execution;
using InflowDataQuery = Inflow.Data.Query;

namespace Inflow.DataService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSingletonInflowDataQuery(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IDataQueryable, Query>(serviceProvider =>
        {
            var databaseProvider = serviceProvider.GetRequiredService<QueryFactory>(); 
            return new InflowDataQuery(databaseProvider);
        });
    }

    public static IServiceCollection AddSingletonDatabaseProvider(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<QueryFactory>(serviceProvider =>
        {
            var sqlOptions = serviceProvider.GetRequiredService<BaseSqlOptions>();
            return new QueryFactory(sqlOptions.DbConnection, sqlOptions.Compiler);
        });
    }

    public static IServiceCollection AddSingletonSqlOptions(this IServiceCollection serviceCollection,
        string sqlOptionsName, string dbConnectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sqlOptionsName, nameof(sqlOptionsName));
        ArgumentException.ThrowIfNullOrWhiteSpace(dbConnectionString, nameof(dbConnectionString));
        return serviceCollection.AddSingleton<BaseSqlOptions>(_ =>
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
            return new Schema(databaseProvider);
        });
    }
}