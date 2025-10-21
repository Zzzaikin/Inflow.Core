using SqlKata.Execution;

namespace Inflow.Data.Schema
{
    public class Schema(QueryFactory databaseProvider) : BaseQuery(databaseProvider), ISchema
    {
        public Task GetAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
