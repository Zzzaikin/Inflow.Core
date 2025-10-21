using Inflow.Common;

namespace Inflow.Data.DTO.DataRequest
{
    public class BaseDataRequestBody
    {
        private readonly string _entityName = null!;

        public string EntityName
        {
            get => _entityName;
            init
            {
                ArgumentNullException.ThrowIfNull(value, nameof(EntityName));
                _entityName = value;
            }
        }
    }
}
