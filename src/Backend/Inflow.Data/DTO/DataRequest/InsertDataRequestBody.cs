namespace Inflow.Data.DTO.DataRequest
{
    public class InsertDataRequestBody : BaseDataRequestBody
    {
        public IEnumerable<Dictionary<string, string>> InsertingData { get; set; }
    }
}
