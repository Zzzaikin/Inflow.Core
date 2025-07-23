using Inflow.Data.DTO.SchemaRequest.BodyItems;

namespace Inflow.Data.DTO.SchemaRequest
{
    public class Schema
    {
        public string Name { get; set; }

        public IEnumerable<Column> Columns { get; set; }
    }
}
