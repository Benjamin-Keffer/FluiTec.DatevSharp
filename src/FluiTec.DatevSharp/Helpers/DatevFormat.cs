using System.Globalization;

namespace FluiTec.DatevSharp.Helpers
{
    /// <summary>
    ///     Formatting rules the DATEV format fixes regardless of the exporting machine. No value takes
    ///     them from the thread culture: a container runs with the invariant culture, a macOS user
    ///     culture such as en-DE has no LCID, and either would change or break the file.
    /// </summary>
    public static class DatevFormat
    {
        /// <summary>   The currency of a DATEV file that states none of its own. </summary>
        public const string DefaultCurrencySymbol = "EUR";

        /// <summary>   DATEV numbers use a comma as decimal separator and no group separator. </summary>
        public static readonly NumberFormatInfo Numbers = NumberFormatInfo.ReadOnly(new NumberFormatInfo
        {
            NumberDecimalSeparator = ",",
            NumberGroupSeparator = string.Empty
        });
    }
}
