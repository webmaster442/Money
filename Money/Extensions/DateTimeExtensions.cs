using System.Globalization;

namespace Money.Extensions
{
    internal static class DateTimeExtensions
    {
        public static DateOnly ToDateOnly(this DateTime date)
        {
            return new DateOnly(date.Year, date.Month, date.Day);
        }

        public static (DateOnly firstDay, DateOnly lastDay) GetMonthDays(this DateTime date)
        {
            DateOnly firstDay = new DateOnly(date.Year, date.Month, 1);
            DateOnly lastDay = firstDay.AddMonths(1).AddDays(-1);
            return (firstDay, lastDay);
        }

        public static (DateOnly firstDay, DateOnly lastDay) GetWeekDays(this DateTime date, CultureInfo culture)
        {
            DayOfWeek currentDay = date.DayOfWeek;
            DayOfWeek startday = culture.DateTimeFormat.FirstDayOfWeek;

            int daysToAdd = 6 - (int)currentDay + (int)startday;

            int diff = currentDay - startday;

            if (diff < 0)
                diff = 6;

            if (daysToAdd == 7)
                daysToAdd = 0;

            DateOnly firstDay = date.ToDateOnly().AddDays(-diff);
            DateOnly lastDay = date.ToDateOnly().AddDays(daysToAdd);


            return (firstDay, lastDay);
        }
    }
}
