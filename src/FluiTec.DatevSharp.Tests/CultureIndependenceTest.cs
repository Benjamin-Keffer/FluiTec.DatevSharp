using System;
using System.Globalization;
using FluiTec.DatevSharp.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.DatevSharp.Tests;

/// <summary>
///     A DATEV file is the same on every machine: a container runs with the invariant culture, a
///     macOS user culture such as en-DE has no LCID, and neither may break or change the file.
/// </summary>
[TestClass]
public class CultureIndependenceTest
{
    private static void UnderCulture(string name, Action action)
    {
        var culture = CultureInfo.CurrentCulture;
        var uiCulture = CultureInfo.CurrentUICulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo(name);
            CultureInfo.CurrentUICulture = new CultureInfo(name);
            action();
        }
        finally
        {
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = uiCulture;
        }
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow("en-DE")]
    [DataRow("en-US")]
    [DataRow("de-DE")]
    public void BookingHeaderStatesEuroUnderAnyCulture(string culture)
    {
        UnderCulture(culture, () =>
        {
            var header = new DatevHeader { DataCategory = DataCategories.Instance.BookingCategory };

            Assert.AreEqual("EUR", header.CurrencySymbol);
        });
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow("en-DE")]
    [DataRow("en-US")]
    [DataRow("de-DE")]
    public void BookingRowStatesEuroUnderAnyCulture(string culture)
    {
        UnderCulture(culture, () => Assert.AreEqual("EUR", new DatevSharp.Rows.BookingRow.BookingRow().CurrencySymbol));
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow("en-US")]
    [DataRow("de-DE")]
    public void NumbersUseADecimalCommaUnderAnyCulture(string culture)
    {
        UnderCulture(culture, () =>
        {
            Assert.AreEqual("1234,5", 1234.5m.ToDatev());
            Assert.AreEqual("-0,25", ((decimal?)-0.25m).ToDatev());
            Assert.AreEqual("12,50", 12.5f.ToDatev());
            Assert.AreEqual("-1234", (-1234).ToDatev());
        });
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow("th-TH")]
    [DataRow("de-DE")]
    public void DatesUseTheGregorianCalendarUnderAnyCulture(string culture)
    {
        UnderCulture(culture, () =>
        {
            var date = new DateTime(2026, 10, 4, 21, 5, 9, 42);

            Assert.AreEqual("20261004", date.ToDatevDate());
            Assert.AreEqual("20261004210509042", date.ToDatevDateTime());
            Assert.AreEqual("2026", date.ToShortDatevYear());
            Assert.AreEqual("04102026", ((DateTime?)date).ToDatevDateReverse());
        });
    }
}
