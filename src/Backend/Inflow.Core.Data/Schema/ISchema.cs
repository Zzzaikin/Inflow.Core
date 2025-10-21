namespace Inflow.Core.Data.Schema;

public interface ISchema
{
    Task GetAsync(string name);
}