using SqlKata.Execution;

namespace Inflow.Core.IntegrationTests.Core.Database;

internal class MsSqlTestDatabasePreparer : ITestDatabasePreparer
{
    public QueryFactory DatabaseProvider { get; }
    public string SqlOptionName { get; }
    public int Timeout { get; }

    public MsSqlTestDatabasePreparer(QueryFactory databaseProvider,string sqlOptionName,  int timeout)
    {
        ArgumentNullException.ThrowIfNull(databaseProvider);
        ArgumentException.ThrowIfNullOrEmpty(sqlOptionName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(timeout);
        DatabaseProvider = databaseProvider;
        SqlOptionName = sqlOptionName;
        Timeout = timeout;
    }
    
    public async Task PrepareAsync(string pathToDumbOrBackup, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}