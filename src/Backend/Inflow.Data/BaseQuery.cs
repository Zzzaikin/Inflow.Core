using Inflow.Data.Options;
using SqlKata.Execution;

namespace Inflow.Data
{
    public abstract class BaseQuery
    {
        protected QueryFactory DatabaseProvider { get; private set; }

        protected BaseQuery(QueryFactory databaseProvider)
        {
            DatabaseProvider = databaseProvider;
        }
    }
}
