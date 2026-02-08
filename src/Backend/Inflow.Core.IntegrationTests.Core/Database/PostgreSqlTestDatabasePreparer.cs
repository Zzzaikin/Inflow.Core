using System.Diagnostics;
using SqlKata.Execution;

namespace Inflow.Core.IntegrationTests.Core.Database;

internal class PostgreSqlTestDatabasePreparer : ITestDatabasePreparer
{
    public QueryFactory DatabaseProvider { get; }
    
    public int Timeout { get; }
    
    public string SqlOptionName { get; }

    public PostgreSqlTestDatabasePreparer(QueryFactory databaseProvider, string sqlOptionName, int timeout)
    {
        ArgumentNullException.ThrowIfNull(databaseProvider);
        ArgumentException.ThrowIfNullOrWhiteSpace(sqlOptionName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(timeout);
        DatabaseProvider =  databaseProvider;
        SqlOptionName = sqlOptionName;
        Timeout = timeout;
    }

    public async Task PrepareAsync(string pathToDumbOrBackup, CancellationToken ct)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pathToDumbOrBackup);

        var connectionParams = DatabaseProvider.Connection.ConnectionString.Split(';');
        var username = connectionParams.FirstOrDefault(x => x.StartsWith("Username="))?.Split('=')[1]
                       ?? throw new InvalidOperationException("Cannot determine database user from connection string");

        var processInfo = new ProcessStartInfo
        {
            FileName = "psql",
            Arguments = $"--dbname={DatabaseProvider.Connection.Database} --file=\"{pathToDumbOrBackup}\"" + 
            $" -U {username} -h localhost -p 5432",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        /* 
         * Explore the options for executing directly via Npgsql so that all authentication methods and 
         * connection string parameters are supported.
         */
        var password = connectionParams.FirstOrDefault(x => x.StartsWith("Password="))?.Split('=')[1]
                   ?? throw new InvalidOperationException("Cannot determine database password from connection string"); 
        processInfo.EnvironmentVariables.Add("PGPASSWORD", password);

        using var process = Process.Start(processInfo) 
            ?? throw new InvalidOperationException("Failed to start pg_restore process");
        
        await process.WaitForExitAsync(ct).ConfigureAwait(false);
        if (process.ExitCode != 0)
        {
            var error = await process.StandardError.ReadToEndAsync(ct).ConfigureAwait(false);
            throw new InvalidOperationException($"pg_restore failed: {error}");
        }
    }
}