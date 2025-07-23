namespace Inflow.Data.Schema
{
    public interface ISchema
    {
        Task GetAsync(string name);
    }
}
