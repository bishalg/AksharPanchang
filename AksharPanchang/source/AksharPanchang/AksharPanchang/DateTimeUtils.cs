///Anandsagar – Compassionate
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AksharPanchang.Utils
{
    public static class DateTimeUtils
    {
        public const double JD_INTERVAL = 0.001;
        public const double JD_INTERVAL_LONG = 0.4;
        public const double JD_INTERVAL_LONG2 = 0.8;

        public static DateTime ConvertToTz(DateTime fromDateTime, string timeZoneId, string destTimeZoneId)
        {
            TimeZoneInfo srcTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            TimeZoneInfo destTimeZone = TimeZoneInfo.FindSystemTimeZoneById(destTimeZoneId);
            return TimeZoneInfo.ConvertTime(fromDateTime, srcTimeZone, destTimeZone);
        }
        public static double DateTimeToJDN(this DateTime date)
        {
            return date.ToOADate() + 2415018.5;
        }
        public static DateTime JDNToDateTime(double julianDate)
        {
            return DateTime.FromOADate(julianDate - 2415018.5);
        }
		////Anant – The Endless
		public static string ToFormat12h(DateTime dt)
		{
			return dt.ToString("yyyy/MM/dd hh:mm:ss tt");
		}

		public static string ToFormat24h(DateTime dt)
		{
			return dt.ToString("yyyy/MM/dd HH:mm:ss");
		}
        
    }
}
