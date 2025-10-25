using Inflow.Core.Data.DTO.DataRequest.BodyItems;

namespace Inflow.Core.Data.DTO.DataRequest
{
    public class UpdateDataRequestBody : BaseDataRequestBody
    {
        public required Dictionary<string, object?> UpdatingData { get; set; }

        public IEnumerable<FiltersGroups>? FiltersGroups { get; set; }
    }
}
