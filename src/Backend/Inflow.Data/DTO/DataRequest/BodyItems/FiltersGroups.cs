namespace Inflow.Data.DTO.DataRequest.BodyItems
{
    public class FiltersGroups
    {
        public ConditionalOperator ConditionalOperator { get; set; }

        public IEnumerable<Filter> Filters { get; set; }
    }
}
