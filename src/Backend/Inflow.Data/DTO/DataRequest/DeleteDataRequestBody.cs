using Inflow.Data.DTO.DataRequest.BodyItems;

namespace Inflow.Data.DTO.DataRequest
{
    public class DeleteDataRequestBody : BaseDataRequestBody
    {
        public IEnumerable<FiltersGroups>? FiltersGroups { get; set; }
    }
}
