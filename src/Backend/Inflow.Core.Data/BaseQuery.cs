using Inflow.Core.Data.DTO.DataRequest;
using SqlKata.Execution;

namespace Inflow.Core.Data;

public abstract class BaseQuery : IDataQueryable
{
    private bool _disposed;
    
    protected QueryFactory DatabaseProvider { get; private set; }

    protected BaseQuery(QueryFactory databaseProvider)
    {
        ArgumentNullException.ThrowIfNull(databaseProvider);
        DatabaseProvider = databaseProvider;
    }
    
    public abstract Task<int> DeleteAsync(DeleteDataRequestBody deleteDataRequestBody);
    public abstract Task<IEnumerable<string>> InsertAsync(InsertDataRequestBody insertDataRequestBody);
    public abstract Task<int> UpdateAsync(UpdateDataRequestBody updateDataRequestBody);
    public abstract Task<IEnumerable<dynamic>> SelectAsync(SelectDataRequestBody selectDataRequestBody);
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
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
}