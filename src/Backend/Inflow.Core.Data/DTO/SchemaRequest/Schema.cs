using Inflow.Core.Data.DTO.SchemaRequest.BodyItems;

namespace Inflow.Core.Data.DTO.SchemaRequest;

public class Schema
{
    public required string Name { get; set; }

    public required IEnumerable<Column> Columns { get; set; }
}