using SqlKata;
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
        var dumb = await File.ReadAllTextAsync(pathToDumbOrBackup, ct);
        var query = new Query().FromRaw(dumb);
        await DatabaseProvider.ExecuteAsync(query, transaction: null, Timeout, ct).ConfigureAwait(false);
    }
}