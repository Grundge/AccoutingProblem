using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace AccoutingProblem.Extensions
{
    public static class ExtensionMethods
    {
        static readonly IEnumerable<string> currencySymbols = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .Select(x => (new RegionInfo(x.LCID)).ISOCurrencySymbol)
            .Distinct()
            .OrderBy(x => x);

        static IEnumerable<T> GetInvalidIso4217<T>(this IEnumerable<T> data, Func<T, bool> predicate)
        {
            
            foreach (T value in data)
            {
                if (!currencySymbols.Contains(value.ToString()))
                {
                    yield return value;
                }
            }
        }
    }
}