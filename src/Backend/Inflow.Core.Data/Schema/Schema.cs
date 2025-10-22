using Inflow.Core.Data.DTO.DataRequest;
using SqlKata.Execution;

namespace Inflow.Core.Data.Schema;

public class Schema(QueryFactory databaseProvider) : BaseQuery(databaseProvider), ISchema
{
    public Task GetAsync(string name)
    {
        throw new NotImplementedException();
    }

    public override Task<int> DeleteAsync(DeleteDataRequestBody deleteDataRequestBody)
    {
        throw new NotImplementedException();
    }

    public override Task<IEnumerable<string>> InsertAsync(InsertDataRequestBody insertDataRequestBody)
    {
        throw new NotImplementedException();
    }

    public override Task<int> UpdateAsync(UpdateDataRequestBody updateDataRequestBody)
    {
        throw new NotImplementedException();
    }

    public override Task<IEnumerable<dynamic>> SelectAsync(SelectDataRequestBody selectDataRequestBody)
    {
        throw new NotImplementedException();
    }
}