using System.Globalization;

namespace Inflow.DataService
{
    public class Configuration
    {
        public required ConnectionStrings ConnectionStrings { get; init; }

        public int MaxSelectedRecordsCount { get; init; }

        public required string SqlOptionsName { get; init; }

        public required string Culture { get; init; }

        public required string[] SupportedCulturesNames { get; init; }

        public IEnumerable<CultureInfo> SupportedCultures
        {
            get
            {
                //TODO: Add SupportedCulturesNames null check.
                return SupportedCulturesNames.Select(supportedCultureName => new CultureInfo(supportedCultureName));
            }
        }
        
        public required string OriginForWhichAllowedAnyMethodAndAnyHeaderInCorsPolicy { get; init; }
    }
}
