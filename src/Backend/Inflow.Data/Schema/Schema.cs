using SqlKata.Execution;

namespace Inflow.Data.Schema
{
    public class Schema : BaseQuery, ISchema
    {
        public Schema(QueryFactory databaseProvider) : base(databaseProvider) { }

        public Task GetAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
