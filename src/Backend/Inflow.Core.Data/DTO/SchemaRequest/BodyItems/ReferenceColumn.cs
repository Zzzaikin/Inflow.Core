namespace Inflow.Core.Data.DTO.SchemaRequest.BodyItems;

public class ReferenceColumn
{
    public required Column RootColumn { get; set; }

    public required string ReferenceSchemaName { get; set; }

    public required Column ForeignColumn { get; set; }
}