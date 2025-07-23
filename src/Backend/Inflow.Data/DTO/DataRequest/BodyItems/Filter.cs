using Inflow.Common;

namespace Inflow.Data.DTO.DataRequest.BodyItems
{
    public class Filter
    {
        private string _column;

        public ConditionalOperator ConditionalOperator { get; set; }

        public ComparisonType ComparisonType { get; set; }

        public string Column
        {
            get
            {
                return _column;
            }

            set
            {
                Argument.IsNotNullOrEmpty(value, nameof(Column));
                _column = value;
            }
        }

        /// <summary>
        /// Value here is a string and it's correct. Even if value will be, for example, int, 
        /// Sqlkata convert to N'5' (example number) and db server grabs it. Therefore if int value comes empty, its 
        /// will be a empty string. Tested by electricity.
        /// </summary>
        public string? Value { get; set; }
    }
}
