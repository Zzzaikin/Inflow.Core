using InflowJoin = Inflow.Data.DTO.DataRequest.BodyItems.Join;
using Inflow.Data.DTO.DataRequest.BodyItems;
using SqlKata.Execution;
using SqlKataQuery = SqlKata.Query;

namespace Inflow.Data.Extensions;

public static class SqlKataQueryExtension
{
    public static async Task<int> UpdateAsync(this SqlKataQuery query,
        Dictionary<string, string> updatingData)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));
        var updatingDataWithUpcastedValue = 
            updatingData.ToDictionary(pair => pair.Key, object (pair) => pair.Value);
        return await query.UpdateAsync(updatingDataWithUpcastedValue);
    }

    public static async Task<IEnumerable<string>> InsertManyGetIdsAsync(this SqlKataQuery query,
        IEnumerable<Dictionary<string, string>> insertingData)
    {
        ArgumentNullException.ThrowIfNull(insertingData, nameof(insertingData));
        var insertedIds = new List<string>();

        foreach (var insertingDataItem in insertingData)
        {
            var insertingDataItemWithUpcastedValue =
                insertingDataItem.ToDictionary(pair => pair.Key, object (pair) => pair.Value);
                
            if (!insertingDataItemWithUpcastedValue.TryGetValue("Id", out var value))
            {
                value = Guid.NewGuid().ToString();;
                insertingDataItemWithUpcastedValue.Add("Id", value);
            }
            var recordId = value.ToString();
            await query.InsertAsync(insertingDataItemWithUpcastedValue);
            insertedIds.Add(recordId!);
        }
        return insertedIds;
    }

    public static SqlKataQuery Join(this SqlKataQuery query, IEnumerable<InflowJoin>? joins)
    {
        if (joins is null) return query;
        foreach (var join in joins)
        {
            var joinedEntityName = join.JoinedEntityName;
            var leftColumnName = join.LeftColumnName;
            var rightColumnName = join.RightColumnName;
            var joinType = join.Type;
                
            switch (joinType)
            {
                case JoinType.Left:
                    query.LeftJoin(joinedEntityName, leftColumnName, rightColumnName);
                    break;
                case JoinType.Inner:
                    query.Join(joinedEntityName, leftColumnName, rightColumnName);
                    break;
                case JoinType.Right:
                    query.RightJoin(joinedEntityName, leftColumnName, rightColumnName);
                    break;
                case JoinType.Cross:
                    query.CrossJoin(joinedEntityName);
                    break;
                case JoinType.Full:
                default:
                    var exceptionMessage = string.Format(Resources.JoinTypeNotImplemented, joinType);
                    throw new NotImplementedException(exceptionMessage);
            }
        }
        return query;
    }

    public static SqlKataQuery Where(this SqlKataQuery query, IEnumerable<FiltersGroups>? filtersGroups)
    {
        if (filtersGroups is null) return query;
        foreach (var filtersGroup in filtersGroups)
        {
            SetOrConditionalOperatorIfExists(query, filtersGroup.ConditionalOperator);
            query.Where(q => SetFiltersFromGroup(q, filtersGroup.Filters));
        }
        return query;
    }

    public static SqlKataQuery OrderBy(this SqlKataQuery query, Order? order)
    {
        if (order is null) return query;
        var orderMode = order.Mode;
        switch (orderMode)
        {
            case OrderMode.Asc:
                query.OrderBy(order.OrderColumnName);
                break;
            case OrderMode.Desc:
                query.OrderByDesc(order.OrderColumnName);
                break;
            default:
                var exceptionMessage = string.Format(Resources.OrderModeNotImplemented, orderMode);
                throw new NotImplementedException(exceptionMessage);
        }
        return query;
    }

    private static void SetOrConditionalOperatorIfExists(SqlKataQuery query, ConditionalOperator conditionalOperator)
    {
        /* Sqlkata allows to add only Or instruction. If do not call Or() it will be And instruction by default.
         * Therefore, field ConditionalOperator can be null, and it's mean that ConditionalOperator will be And.
         */
        if (conditionalOperator == ConditionalOperator.Or)
        {
            query.Or();
        }
    }

    private static SqlKataQuery SetFiltersFromGroup(SqlKataQuery query, IEnumerable<Filter> filters)
    {
        foreach (var filter in filters)
        {
            SetOrConditionalOperatorIfExists(query, filter.ConditionalOperator);

            var comparisonType = filter.ComparisonType;
            var filterColumn = filter.Column;
            var filterValue = filter.Value;

            switch (filter.ComparisonType)
            {
                case ComparisonType.Equal:
                    ArgumentNullException.ThrowIfNull(filterValue, nameof(filterValue));
                    query.Where(filterColumn, "=", filterValue);
                    break;
                case ComparisonType.NotEqual:
                    ArgumentNullException.ThrowIfNull(filterValue, nameof(filterValue));
                    query.WhereNot(filterColumn, "=", filterValue);
                    break;
                case ComparisonType.IsNull:
                    query.WhereNull(filterColumn);
                    break;
                case ComparisonType.NotNull:
                    query.WhereNotNull(filterColumn);
                    break;
                case ComparisonType.More:
                    ArgumentNullException.ThrowIfNull(filterValue, nameof(filterValue));
                    query.Where(filterColumn, ">", filterValue);
                    break;
                case ComparisonType.Less:
                    ArgumentNullException.ThrowIfNull(filterValue, nameof(filterValue));
                    query.Where(filterColumn, "<", filterValue);
                    break;
                case ComparisonType.In:
                case ComparisonType.NotIn:
                default:
                    var exceptionMessage = string.Format(Resources.ComparisonTypeNotImpemented, comparisonType);
                    throw new NotImplementedException(exceptionMessage);
            }
        }
        return query;
    }
}