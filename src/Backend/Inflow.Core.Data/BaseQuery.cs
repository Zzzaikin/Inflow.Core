using SqlKata.Execution;

namespace Inflow.Core.Data;

public abstract class BaseQuery : IDisposable
{
    private bool _disposed;
    
    protected QueryFactory DatabaseProvider { get; private set; }

    protected BaseQuery(QueryFactory databaseProvider)
    {
        ArgumentNullException.ThrowIfNull(databaseProvider);
        DatabaseProvider = databaseProvider;
    }

    public void Dispose()
    {
        if (_disposed) return;
        DatabaseProvider.Dispose();
        GC.SuppressFinalize(this);
        DatabaseProvider = null!;
        _disposed = true;
    }
}