using Inflow.Core.Data.DTO.DataRequest.BodyItems;

namespace Inflow.Core.Data.DTO.DataRequest
{
    public class DeleteDataRequestBody : BaseDataRequestBody
    {
        public IEnumerable<FiltersGroups>? FiltersGroups { get; set; }
    }
}
