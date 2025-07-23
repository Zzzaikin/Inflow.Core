using SqlKata.Execution;
using Inflow.Data.Options;
using Inflow.Data.Extensions;
using Inflow.Data.DTO.DataRequest;

namespace Inflow.Data
{
    public class Query : BaseQuery, IDataQueryable
    {
        public Query(QueryFactory databaseProvider) : base(databaseProvider) { }

        public async Task<int> DeleteAsync(DeleteDataRequestBody deleteDataRequestBody)
        {
            var affectedRecordCount = await DatabaseProvider.Query(deleteDataRequestBody.EntityName)
                .Where(filtersGroups: deleteDataRequestBody.FiltersGroups)
                .DeleteAsync();

            return affectedRecordCount;
        }

        public async Task<IEnumerable<string>> InsertAsync(InsertDataRequestBody insertDataRequestBody)
        {
            var insertedRecordsIds = await DatabaseProvider.Query(insertDataRequestBody.EntityName)
                .InsertManyGetIdsAsync(insertDataRequestBody.InsertingData);

            return insertedRecordsIds;
        }

        public async Task<IEnumerable<dynamic>> SelectAsync(SelectDataRequestBody selectDataRequestBody)
        {
            var records = await DatabaseProvider.Query()
                .Select(selectDataRequestBody.ColumnNames.ToArray())
                .From(selectDataRequestBody.EntityName)
                .Join(joins: selectDataRequestBody.Joins)
                .Where(filtersGroups: selectDataRequestBody.FiltersGroups)
                .OrderBy(order: selectDataRequestBody.Order)
                .GetAsync();

            return records;
        }

        public async Task<int> UpdateAsync(UpdateDataRequestBody updateDataRequestBody)
        {
            var affectedRecordsCount = await DatabaseProvider.Query(updateDataRequestBody.EntityName)
                .Where(filtersGroups: updateDataRequestBody.FiltersGroups)
                .UpdateAsync(updateDataRequestBody.UpdatingData);

            return affectedRecordsCount;
        }
    }
}
