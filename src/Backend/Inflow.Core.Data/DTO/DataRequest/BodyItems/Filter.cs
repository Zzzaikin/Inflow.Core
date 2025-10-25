namespace Inflow.Core.Data.DTO.DataRequest.BodyItems;

public class Filter
{
    private readonly string _column = null!;

    public ConditionalOperator ConditionalOperator { get; set; }

    public ComparisonType ComparisonType { get; set; }

    public string Column
    {
        get => _column;

        init
        {
            ArgumentNullException.ThrowIfNull(value, nameof(Column));
            _column = value;
        }
    }
    
    public object? Value { get; set; }
}