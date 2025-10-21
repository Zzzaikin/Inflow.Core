using SqlKata.Execution;

namespace Inflow.Core.Data;

public abstract class BaseQuery(QueryFactory databaseProvider)
{
    protected QueryFactory DatabaseProvider { get; private set; } = databaseProvider;
}