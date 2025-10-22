using Inflow.Core.Data.DTO.DataRequest;

namespace Inflow.Core.Data;

public interface IDataQueryable : IDisposable
{
    Task<int> DeleteAsync(DeleteDataRequestBody deleteDataRequestBody);

    Task<IEnumerable<string>> InsertAsync(InsertDataRequestBody insertDataRequestBody);

    Task<int> UpdateAsync(UpdateDataRequestBody updateDataRequestBody);

    Task<IEnumerable<dynamic>> SelectAsync(SelectDataRequestBody selectDataRequestBody);
}