using Inflow.Common;

namespace Inflow.Data.DTO.DataRequest
{
    public class BaseDataRequestBody
    {
        private string _entityName;

        public string EntityName
        {
            get
            {
                return _entityName;
            }

            set
            {
                Argument.IsNotNullOrEmpty(value, nameof(EntityName));
                _entityName = value;
            }
        }
    }
}
