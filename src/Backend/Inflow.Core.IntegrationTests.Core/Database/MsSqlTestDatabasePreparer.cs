using Inflow.Core.Data;

namespace Inflow.Core.IntegrationTests.Core.Database;

internal class MsSqlTestDatabasePreparer : ITestDatabasePreparer
{
    public string SqlOptionName { get; }
    
    public int Timeout { get; }

    public MsSqlTestDatabasePreparer(string sqlOptionName,  int timeout)
    {
        ArgumentException.ThrowIfNullOrEmpty(sqlOptionName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(timeout);
        SqlOptionName = sqlOptionName;
    }
    
    public async Task PrepareAsync(string pathToDumbOrBackup, CancellationToken ct)
    {
        //throw new NotImplementedException();
    }
}