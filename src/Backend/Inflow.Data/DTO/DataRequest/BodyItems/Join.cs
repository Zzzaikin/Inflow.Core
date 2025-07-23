using Inflow.Common;

namespace Inflow.Data.DTO.DataRequest.BodyItems
{
    public class Join
    {
        private string _joinedEntityName;

        private string _leftColumnName;

        private string _rightColumnName;

        public string RightColumnName
        {
            get
            {
                return _rightColumnName;
            }

            set
            {
                Argument.IsNotNullOrEmpty(value, nameof(RightColumnName));
                _rightColumnName = value;
            }
        }

        public string LeftColumnName
        {
            get
            {
                return _leftColumnName;
            }

            set
            {
                Argument.IsNotNullOrEmpty(value, nameof(LeftColumnName));
                _leftColumnName = value;
            }
        }

        public string JoinedEntityName
        {
            get
            {
                return _joinedEntityName;
            }

            set
            {
                Argument.IsNotNullOrEmpty(value, nameof(JoinedEntityName));
                _joinedEntityName = value;
            }
        }

        public JoinType Type { get; set; }
    }
}
