using Inflow.Data.DTO.DataRequest;

namespace Inflow.DataService
{
    public interface IQueryExecutable
    {
        Task<IEnumerable<dynamic>> SelectAsync(BaseDataRequestBody dataRequestBody);

        Task<int> UpdateAsync(BaseDataRequestBody dataRequestBody);

        Task<int> DeleteAsync(BaseDataRequestBody dataRequestBody);

        Task<int> InsertAsync(BaseDataRequestBody dataRequestBody);
    }
}
