using Inflow.Core.Data.DTO.DataRequest.BodyItems;
using SqlKata.Execution;
using SqlKataQuery = SqlKata.Query;

namespace Inflow.Core.Data.Extensions;

public static class SqlKataQueryExtension
{
    public static async Task<int> UpdateWithConversionOfValueTypesAsync(this SqlKataQuery query,
        Dictionary<string, object?> updatingData)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));
        var typedUpdatingData = GetTypedData(updatingData);
        return await query.UpdateAsync(typedUpdatingData);
    }

    public static async Task<IEnumerable<string>> InsertWithConversionOfValueTypesAsync(this SqlKataQuery query,
        IEnumerable<Dictionary<string, object?>> insertingData)
    {
        ArgumentNullException.ThrowIfNull(insertingData, nameof(insertingData));
        var insertedIds = new List<string>();
        
        foreach (var insertingDataItem in insertingData)
        {
            var typedInsertingDataItem = GetTypedData(insertingDataItem);
            if (!typedInsertingDataItem.TryGetValue("Id", out var value))
            {
                value = Guid.NewGuid();
                typedInsertingDataItem.Add("Id", value);
            }
            else if (value is null)
            {
                value = Guid.NewGuid();
                typedInsertingDataItem["Id"] = value;
            }

            var recordId = value?.ToString();
            await query.InsertAsync(typedInsertingDataItem);
            insertedIds.Add(recordId!);
        }
        return insertedIds;
    }

    public static SqlKataQuery Join(this SqlKataQuery query, IEnumerable<Join>? joins)
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

    /// <summary>
    /// Sqlkata allows to add only Or operator. If you do not set Or it will be And operator by default.
    /// Therefore, field ConditionalOperator can be null, and it's mean that ConditionalOperator will be And.
    /// </summary>
    private static void SetOrConditionalOperatorIfExists(SqlKataQuery query, ConditionalOperator conditionalOperator)
    {
        if (conditionalOperator == ConditionalOperator.Or)
        {
            query.Or();
        }
    }

    private static Dictionary<string, object?> GetTypedData(Dictionary<string, object?> rawData)
    {
        return rawData.ToDictionary((pair) => pair.Key, object? (pair) => GetTypedValue(pair.Value));
    }

    /// <summary>
    /// Returns typed value (null, Guid, bool, DateTime, int, double, decimal or string). String will be if others
    /// types have not parsed.
    /// </summary>
    private static object? GetTypedValue(object? rawData)
    {
        if (rawData is null) return null;
        var stringValue = rawData.ToString();
        if (Guid.TryParse(stringValue, out var guidValue)) return guidValue;
        if (bool.TryParse(stringValue, out var boolValue)) return boolValue;
        if (DateTime.TryParse(stringValue, out var dateTimeValue)) return dateTimeValue;
        if (int.TryParse(stringValue, out var intValue)) return intValue;
        if (double.TryParse(stringValue, out var doubleValue)) return doubleValue;
        if (decimal.TryParse(stringValue, out var decimalValue)) return decimalValue;
        return stringValue;
    }

    private static SqlKataQuery SetFiltersFromGroup(SqlKataQuery query, IEnumerable<Filter> filters)
    {
        foreach (var filter in filters)
        {
            SetOrConditionalOperatorIfExists(query, filter.ConditionalOperator);

            var comparisonType = filter.ComparisonType;
            var filterColumn = filter.Column;
            var filterValue = GetTypedValue(filter.Value);

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