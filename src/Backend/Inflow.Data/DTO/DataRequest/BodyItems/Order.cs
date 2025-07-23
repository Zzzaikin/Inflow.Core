namespace Inflow.Data.DTO.DataRequest.BodyItems
{
    public class Order
    {
        public OrderMode Mode { get; set; }

        public string OrderColumnName { get; set; } = "Id";
    }
}
