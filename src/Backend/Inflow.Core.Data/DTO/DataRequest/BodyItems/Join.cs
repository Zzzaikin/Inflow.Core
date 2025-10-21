namespace Inflow.Core.Data.DTO.DataRequest.BodyItems;

public class Join
{
    private readonly string _joinedEntityName = null!;
    private readonly string _leftColumnName = null!;
    private readonly string _rightColumnName = null!;

    public string RightColumnName
    {
        get => _rightColumnName;
        init
        {
            ArgumentNullException.ThrowIfNull(value, nameof(RightColumnName));
            _rightColumnName = value;
        }
    }

    public string LeftColumnName
    {
        get => _leftColumnName;
        init
        {
            ArgumentNullException.ThrowIfNull(value, nameof(LeftColumnName));
            _leftColumnName = value;
        }
    }

    public string JoinedEntityName
    {
        get => _joinedEntityName;
        init
        {
            ArgumentNullException.ThrowIfNull(value,  nameof(JoinedEntityName));
            _joinedEntityName = value;
        }
    }

    public JoinType Type { get; init; }
}