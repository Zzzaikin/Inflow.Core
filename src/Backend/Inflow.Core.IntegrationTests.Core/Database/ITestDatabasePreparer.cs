using Microsoft.Extensions.Configuration;

namespace Inflow.Core.IntegrationTests.Core.Database;

internal interface ITestDatabasePreparer
{
    string SqlOptionName { get; }
    
    int Timeout { get; }
    
    Task PrepareAsync(string pathToDumbOrBackup, CancellationToken ct);
}