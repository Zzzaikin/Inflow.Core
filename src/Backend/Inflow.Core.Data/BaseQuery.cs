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

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            DatabaseProvider.Dispose();
            DatabaseProvider = null!;
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}