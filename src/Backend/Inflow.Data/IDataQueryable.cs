using Inflow.Data.DTO.DataRequest;

namespace Inflow.Data
{
    public interface IDataQueryable
    {
        Task<int> DeleteAsync(DeleteDataRequestBody deleteDataRequestBody);

        Task<IEnumerable<string>> InsertAsync(InsertDataRequestBody insertDataRequestBody);

        Task<int> UpdateAsync(UpdateDataRequestBody updateDataRequestBody);

        Task<IEnumerable<dynamic>> SelectAsync(SelectDataRequestBody selectDataRequestBody);
    }
}
