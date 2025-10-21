namespace Inflow.Core.Data.DTO.SchemaRequest.BodyItems;

public class Column
{
    public required string Name { get; set; }

    public ColumnType Type { get; set; }
}