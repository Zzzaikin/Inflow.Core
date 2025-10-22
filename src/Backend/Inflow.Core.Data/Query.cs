using Inflow.Core.Data.DTO.DataRequest;
using Inflow.Core.Data.Extensions;
using System.Diagnostics.CodeAnalysis;
using SqlKata.Execution;

namespace Inflow.Core.Data;

public class Query(QueryFactory databaseProvider) : BaseQuery(databaseProvider)
{
    public override async Task<int> DeleteAsync(DeleteDataRequestBody deleteDataRequestBody)
    {
        return await DatabaseProvider.Query(deleteDataRequestBody.EntityName)
            .Where(filtersGroups: deleteDataRequestBody.FiltersGroups)
            .DeleteAsync();
    }

    public override async Task<IEnumerable<string>> InsertAsync(InsertDataRequestBody insertDataRequestBody)
    {
        return await DatabaseProvider.Query(insertDataRequestBody.EntityName)
            .InsertManyGetIdsAsync(insertDataRequestBody.InsertingData);
            
    }

    [SuppressMessage("Usage", "CA2208:Instantiate argument exceptions correctly")]
    public override async Task<IEnumerable<dynamic>> SelectAsync(SelectDataRequestBody selectDataRequestBody)
    {
        ArgumentNullException.ThrowIfNull(selectDataRequestBody, nameof(selectDataRequestBody));
        ArgumentNullException.ThrowIfNull(selectDataRequestBody.ColumnNames, nameof(selectDataRequestBody.ColumnNames));
        return await DatabaseProvider.Query()
            .Select(selectDataRequestBody.ColumnNames.ToArray())
            .From(selectDataRequestBody.EntityName)
            .Join(joins: selectDataRequestBody.Joins)
            .Where(filtersGroups: selectDataRequestBody.FiltersGroups)
            .OrderBy(order: selectDataRequestBody.Order)
            .GetAsync();
    }

    public override async Task<int> UpdateAsync(UpdateDataRequestBody updateDataRequestBody)
    {
        ArgumentNullException.ThrowIfNull(updateDataRequestBody, nameof(updateDataRequestBody));
        return await DatabaseProvider.Query(updateDataRequestBody.EntityName)
            .Where(filtersGroups: updateDataRequestBody.FiltersGroups)
            .UpdateAsync(updateDataRequestBody.UpdatingData);
    }
}