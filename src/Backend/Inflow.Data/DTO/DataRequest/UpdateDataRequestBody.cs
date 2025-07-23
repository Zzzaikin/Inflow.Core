using Inflow.Data.DTO.DataRequest.BodyItems;

namespace Inflow.Data.DTO.DataRequest
{
    public class UpdateDataRequestBody : BaseDataRequestBody
    {
        public Dictionary<string, string> UpdatingData { get; set; }

        public IEnumerable<FiltersGroups>? FiltersGroups { get; set; }
    }
}
