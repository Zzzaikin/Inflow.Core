using System.Globalization;

namespace Inflow.DataService
{
    public class Configuration
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public int MaxSelectedRecordsCount { get; set; }

        public string SqlOptionsName { get; set; }

        public string Culture { get; set; }

        public string[] SupportedCulturesNames { get; set; }

        public IEnumerable<CultureInfo> SupportedCultures
        {
            get
            {
                //TODO: Add SupportedCulturesNames null check.
                foreach (var supportedCultureName in SupportedCulturesNames)
                {
                    yield return new CultureInfo(supportedCultureName);
                }
            }
        }
        
        public string OriginForWhichAllowedAnyMethodAndAnyHeaderInCorsPolicy { get; set; }
    }
}
