using FluiTec.DatevSharp.Attributes;
using FluiTec.DatevSharp.Helpers;
using FluiTec.DatevSharp.Interfaces;
using FluiTec.DatevSharp.Rows.Enums;
using FluiTec.DatevSharp.Rows.Maps;

namespace FluiTec.DatevSharp.Rows.BookingRow
{
    /// <summary>   A booking row. </summary>
    [DatevRow(typeof(BookingMap), typeof(HeaderRow))]
    public partial class BookingRow : IDatevRow
    {
        /// <summary>   Default constructor. </summary>
        public BookingRow()
        {
            Claim = Claim.Debit;
            CurrencySymbol = DatevFormat.DefaultCurrencySymbol;
            Fixing = false;
        }
    }
}