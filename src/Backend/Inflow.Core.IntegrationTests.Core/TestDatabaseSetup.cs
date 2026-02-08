using Inflow.Core.Data.Extensions;
using Inflow.Core.IntegrationTests.Core.Configuration;
using Inflow.Core.IntegrationTests.Core.Extensions;
using Inflow.Core.IntegrationTests.Core.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Inflow.Core.IntegrationTests.Core;

public class TestDatabaseSetup
{
    private IEnumerable<Dbms> _dbmss = [];
    private static IHost? _host;

    public static IConfiguration Configuration { get; private set; } = default!;
    public static List<IServiceProvider> DatabaseServiceProvidersPerDbms { get; private set; } = [];
    public static IServiceProvider ServiceProvider { get; private set; } = default!;

    [OneTimeSetUp]
    public virtual async Task SetUp()
    {
        SetupHost();
        InitConfiguration();
        SetupDiContainersForDbmss();
        var cts = new CancellationTokenSource();
        var prepareDbTasks = PrepareDatabasesAsync(cts);
        //TODO: Add Error handling and logging. Now there is only operation canceled if some problem occurs.
        await Task.WhenAll(prepareDbTasks).WaitAsync(cts.Token).ConfigureAwait(false);
    }

    [OneTimeTearDown]
    public virtual async Task TearDown()
    {
        // TODO: Clear the databases after all tests have run. Implement ITestDatabaseCleaner and execute it here.
        foreach (var databaseServiceProvider in DatabaseServiceProvidersPerDbms)
        {
            if (databaseServiceProvider is IAsyncDisposable asyncDisposableDatabaseServiceProvider)
                await asyncDisposableDatabaseServiceProvider.DisposeAsync();
            else if (databaseServiceProvider is IDisposable syncDisposableDatabaseServiceProvider)
                syncDisposableDatabaseServiceProvider.Dispose();
        }

        if (_host is not null)
        {
            // TODO: Add Error handling and logging.
            try { await _host.StopAsync(); } catch { }
            if (_host is IAsyncDisposable asyncHost)
                await asyncHost.DisposeAsync();
            else
                _host.Dispose();
        }
    }

    private void InitConfiguration()
    {
        ArgumentNullException.ThrowIfNull(Configuration);
        _dbmss = Configuration.GetSection("DbmsSettings").GetSection("Dbmss").Get<IEnumerable<Dbms>>()
                 ?? throw new ArgumentNullException(nameof(_dbmss));
    }

    private void SetupHost()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((ctx, builder) =>
            {
                builder.SetBasePath(AppContext.BaseDirectory)
                       .AddJsonFile("testsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Debug);
            })
            .Build();

        ServiceProvider = _host.Services;
        Configuration = ServiceProvider.GetRequiredService<IConfiguration>();
    }

    private void SetupDiContainersForDbmss()
    {
        foreach (var dbms in _dbmss!)
        {
            var sqlOptionsName = dbms.SqlOptionsName;

            var services = new ServiceCollection();

            services
                .AddSingleton(Configuration)
                .AddScopedSqlOptions(sqlOptionsName, dbms.DbConnectionString)
                .AddScopedDatabaseProvider()
                .AddScopedInflowDataQuery()
                .AddTransientTestDatabasePreparer(sqlOptionsName, dbms.Timeout);

            var serviceProvider = services.BuildServiceProvider();
            DatabaseServiceProvidersPerDbms.Add(serviceProvider);
        }
    }

    private IEnumerable<Task> PrepareDatabasesAsync(CancellationTokenSource cts)
    {
        return DatabaseServiceProvidersPerDbms.Select(serviceProvider => Task.Run(async () =>
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                /* 
                 * There is opening new connection to db. Now it's redudant, but later it will be used for 
                 * implementation with Npgsql (now ProcessStartInfo - see dataPreparer.PrepareAsync).
                 * TODO: Implement database preparation directly via Npgsql so that all authentication methods 
                 * and connection string parameters are supported.
                 */
                var dataPreparer = scope.ServiceProvider.GetRequiredService<ITestDatabasePreparer>();
                var dbms = _dbmss.First(db => db.SqlOptionsName == dataPreparer.SqlOptionName);
                var pathToDumbOrBackup = Path.Combine(AppContext.BaseDirectory, dbms.RelativePathToBackup);
                await dataPreparer.PrepareAsync(pathToDumbOrBackup, cts.Token).ConfigureAwait(false);
            }
            catch
            {
                await cts.CancelAsync();
                throw;
            }
        }));
    }
}