namespace Inflow.Core.Data.DTO.DataRequest;

public class InsertDataRequestBody : BaseDataRequestBody
{
    public required IEnumerable<Dictionary<string, object?>> InsertingData { get; set; }
}