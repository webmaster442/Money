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
            var firstDay = new DateOnly(date.Year, date.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);
            return (firstDay, lastDay);
        }
    }
}
