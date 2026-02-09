using SqlKata.Execution;

namespace Inflow.Core.IntegrationTests.Core.Database;

internal interface ITestDatabasePreparer
{
    QueryFactory DatabaseProvider { get; }
    string SqlOptionName { get; }
    int Timeout { get; }
    Task PrepareAsync(string pathToDumbOrBackup, CancellationToken ct);
}