using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace KPMGAssignment.Helpers
{
    public static class CurrencyHelper
    {
        public static readonly List<string> ISOCurrencySymbols;

        static CurrencyHelper()
        {
            ISOCurrencySymbols = CultureInfo.GetCultures(CultureTypes.SpecificCultures) //Only specific cultures contain region information
                                    .Select(x => (new RegionInfo(x.LCID)).ISOCurrencySymbol)
                                    .Distinct()
                                    .OrderBy(x => x).ToList();
        }
    }
}
