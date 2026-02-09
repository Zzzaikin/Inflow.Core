using Inflow.Core.Data.Options;
using Inflow.Core.Data.Schema;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Execution;
using InflowDataQuery = Inflow.Core.Data.Query;

namespace Inflow.Core.Data.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddScopedInflowDataQuery(this IServiceCollection serviceCollection)
        => serviceCollection.AddScoped<IDataQueryable>(GetInflowDataQuery);
    
    public static IServiceCollection AddTransientInflowDataQuery(this IServiceCollection serviceCollection)
        => serviceCollection.AddTransient<IDataQueryable>(GetInflowDataQuery);

    public static IServiceCollection AddScopedDatabaseProvider(this IServiceCollection serviceCollection)
        => serviceCollection.AddScoped(GetDatabaseProvider);
    
    public static IServiceCollection AddTransientDatabaseProvider(this IServiceCollection serviceCollection)
        => serviceCollection.AddTransient(GetDatabaseProvider);

    public static IServiceCollection AddScopedSqlOptions(this IServiceCollection serviceCollection,
        string sqlOptionsName, string dbConnectionString)
        => serviceCollection.AddScoped<BaseSqlOptions>(_ => GetSqlOptions(sqlOptionsName, dbConnectionString));

    public static IServiceCollection AddTransientSqlOptions(this IServiceCollection serviceCollection,
        string sqlOptionsName, string dbConnectionString)
        => serviceCollection.AddTransient<BaseSqlOptions>(_ => GetSqlOptions(sqlOptionsName, dbConnectionString));

    public static IServiceCollection AddSingletonSqlSchema(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<ISchema>(serviceProvider =>
        {
            var databaseProvider = serviceProvider.GetRequiredService<QueryFactory>();
            return new Schema.Schema(databaseProvider);
        });
    }
    
    private static BaseSqlOptions GetSqlOptions(string sqlOptionsName, string dbConnectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sqlOptionsName);
        ArgumentException.ThrowIfNullOrWhiteSpace(dbConnectionString);
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
    }
    
    private static QueryFactory GetDatabaseProvider(IServiceProvider serviceProvider)
    {
        var sqlOptions = serviceProvider.GetRequiredService<BaseSqlOptions>();
        return new QueryFactory(sqlOptions.DbConnection, sqlOptions.Compiler);
    }

    private static InflowDataQuery GetInflowDataQuery(IServiceProvider serviceProvider)
    {
        var databaseProvider = serviceProvider.GetRequiredService<QueryFactory>(); 
        return new InflowDataQuery(databaseProvider);
    }
}