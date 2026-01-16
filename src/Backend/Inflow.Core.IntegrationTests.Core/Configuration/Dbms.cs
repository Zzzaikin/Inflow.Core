namespace Inflow.Core.IntegrationTests.Core.Configuration;

public class Dbms
{
    public required string DbConnectionString  { get; set; }
    
    public required string SqlOptionsName { get; set; }
    
    public required string RelativePathToBackup {get; set; }
    
    public required int Timeout { get; set; }
}