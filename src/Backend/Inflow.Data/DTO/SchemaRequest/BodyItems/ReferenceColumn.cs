namespace Inflow.Data.DTO.SchemaRequest.BodyItems
{
    public class ReferenceColumn
    {
        public Column RootColumn { get; set; }

        public string ReferenceSchemaName { get; set; }

        public Column ForeignColumn { get; set; }
    }
}
