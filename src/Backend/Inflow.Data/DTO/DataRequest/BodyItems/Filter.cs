namespace Inflow.Data.DTO.DataRequest.BodyItems;

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

    /// <summary>
    /// Value here is a string and it's correct. Even if value will be, for example, int, 
    /// Sqlkata convert to N'5' (example number) and db server grabs it. Therefore, if int value comes empty, its 
    /// will be empty string. Tested by electricity.
    /// </summary>
    public string? Value { get; set; }
}