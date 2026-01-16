using Inflow.Core.Data.Extensions;
using Inflow.Core.IntegrationTests.Core.Configuration;
using Inflow.Core.IntegrationTests.Core.Extensions;
using Inflow.Core.IntegrationTests.Core.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Inflow.Core.IntegrationTests.Core;

[SetUpFixture]
public class TestDatabaseSetup
{
    private IEnumerable<Dbms> _dbmss;
    
    public static IConfiguration Configuration { get; private set; }
    public static List<IServiceProvider> DatabaseServiceProvidersPerDbms { get; private set; }
    public static IServiceProvider ServiceProvider { get; private set; }
    
    [OneTimeSetUp]
    public async Task GlobalSetUp()
    {
        InitConfiguration();
        SetupDiContainer();
        SetupDiContainersForDbmss();
        var prepareDbTasks = PrepareDatabasesAsync(out var ct);
        await Task.WhenAll(prepareDbTasks).WaitAsync(ct).ConfigureAwait(false);
    }
    
    [OneTimeTearDown]
    public async Task GlobalTearDown()
    {
        await (ServiceProvider as IAsyncDisposable)!.DisposeAsync();
        // Очистка базы данных после всех тестов
    }

    private void InitConfiguration()
    {
        Configuration = 
            new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("testsettings.json", optional: false, reloadOnChange: true)
                .Build();
        ArgumentNullException.ThrowIfNull(nameof(Configuration));
        
        _dbmss = Configuration.GetSection("DbmsSettings").GetSection("Dbmss").Get<IEnumerable<Dbms>>()
                 ?? throw new ArgumentNullException(nameof(_dbmss));
    }

    private void SetupDiContainer()
    {
        var serviceCollection = new ServiceCollection();
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private void SetupDiContainersForDbmss()
    {
        DatabaseServiceProvidersPerDbms = new List<IServiceProvider>();
        foreach (var dbms in _dbmss!)
        {
            var sqlOptionsName = dbms.SqlOptionsName;
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddScopedSqlOptions(sqlOptionsName, dbms.DbConnectionString)
                .AddScopedDatabaseProvider()
                .AddScopedInflowDataQuery()
                .AddTransientTestDatabasePreparer(sqlOptionsName, dbms.Timeout);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            DatabaseServiceProvidersPerDbms.Add(serviceProvider);
        }
    }

    private IEnumerable<Task> PrepareDatabasesAsync(out CancellationToken ct)
    {
        var cts = new CancellationTokenSource();
        ct = cts.Token;
        
        return DatabaseServiceProvidersPerDbms.Select(serviceProvider =>
        {
            var dataPreparer = serviceProvider.GetRequiredService<ITestDatabasePreparer>();
            var dbms = _dbmss.First(dbms => dbms.SqlOptionsName == dataPreparer.SqlOptionName);
            var pathToDumbOrBackup = Path.Combine(AppContext.BaseDirectory, dbms.RelativePathToBackup);
            var lambdaCt = cts.Token;
            
            try
            {
                return dataPreparer.PrepareAsync(pathToDumbOrBackup, lambdaCt);
            }
            catch
            {
                cts.Cancel();
                throw;
            }
        });
    }
}