using Inflow.Core.Data;
using Inflow.Core.Data.Extensions;
using Inflow.Core.IntegrationTests.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Compilers;

namespace Inflow.Core.IntegrationTests.Core;

[SetUpFixture]
public class GlobalSetup
{
    private IList<IDataQueryable> _queryProviders;

    internal static IConfiguration Configuration;
    internal static IDictionary<string, IServiceProvider> ServiceProvidersPerDbms;
    
    [OneTimeSetUp]
    public void SetUp()
    {
        InitConfiguration();
        SetupDiContainersForDbmss();
        Task.WaitAll(CreatePostgreSqlTestDb(), CreateMSSqlTestDb());
    }

    private void CreatePostgreSqlTestDb()
    {
        var queryProvider = 
            _queryProviders.First(query => query.DatabaseProvider.Compiler is PostgresCompiler);
        var pathTo
        CreateTestDb(queryProvider);
    }

    private void InitConfiguration()
    {
        Configuration = 
            new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("testsettings.json", optional: false, reloadOnChange: true)
                .Build();
        ArgumentNullException.ThrowIfNull(nameof(Configuration));
    }

    private void SetupDiContainersForDbmss()
    {
        var dbmsParameters = Configuration.GetSection("DbmsSettings").Get<IEnumerable<Dbms>>();
        ArgumentNullException.ThrowIfNull(nameof(dbmsParameters));
        ServiceProvidersPerDbms = new Dictionary<string, IServiceProvider>();
        _queryProviders = new List<IDataQueryable>();
        
        foreach (var dbmsParameter in dbmsParameters!)
        {
            var sqlOptionsName = dbmsParameter.SqlOptionsName;
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingletonSqlOptions(sqlOptionsName, dbmsParameter.DbConnectionString)
                .AddSingletonDatabaseProvider()
                .AddSingletonInflowDataQuery();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            ServiceProvidersPerDbms.Add(sqlOptionsName, serviceProvider);
            _queryProviders.Add(serviceProvider.GetRequiredService<IDataQueryable>());
        }
    }
}