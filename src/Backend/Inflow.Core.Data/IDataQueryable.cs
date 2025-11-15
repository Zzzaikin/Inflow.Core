using Inflow.Core.Data.DTO.DataRequest;
using SqlKata.Execution;

namespace Inflow.Core.Data;

public interface IDataQueryable : IDisposable
{
    QueryFactory  DatabaseProvider { get; }
    
    Task<int> DeleteAsync(DeleteDataRequestBody deleteDataRequestBody);

    Task<IEnumerable<string>> InsertAsync(InsertDataRequestBody insertDataRequestBody);

    Task<int> UpdateAsync(UpdateDataRequestBody updateDataRequestBody);

    Task<IEnumerable<dynamic>> SelectAsync(SelectDataRequestBody selectDataRequestBody);
}