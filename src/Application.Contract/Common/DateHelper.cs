using System.Globalization;

namespace EngTech.Application.Contract.Common;

public static class DateHelperExtensions
{
    private static readonly PersianCalendar pc = new();


    public static string GetShortNumericPersianDate(this DateTime date)
    {
        return pc.GetYear(date) + "/" + pc.GetMonth(date) + "/" + pc.GetDayOfMonth(date);
    }

    public static Tuple<DateTime, DateTime> FromEpochToDate(this long unixTime)
    {
        DateTimeOffset dateTimeOffSet = DateTimeOffset.FromUnixTimeMilliseconds(unixTime);
        DateTime from = dateTimeOffSet.DateTime.Date.AddHours(0).AddMinutes(0).AddSeconds(0);
        DateTime to = dateTimeOffSet.DateTime.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        return new Tuple<DateTime, DateTime>(from, to);
    }

    public static long ToJsTime(this long csTicks)
    {
        if (csTicks < 0)
        {
            return 621355968000000000 / 10000;
        }

        return (csTicks - 621355968000000000) / 10000;
    }

    public static long ToJsTime(this DateTime csTime)
    {
        return ToJsTime(csTime.Ticks);
    }

    public static long ToCSTicks(this long JsTime)
    {
        return (JsTime * 10000) + 621355968000000000;
    }

    public static DateTime ToCSTime(this long JsTime)
    {
        return new DateTime(ToCSTicks(JsTime)).ToUniversalTime();
    }
}