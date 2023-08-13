namespace Money.Web
{
    public static class Extensions
    {
        public static DateOnly ToDateOnly(this DateTime date) 
        {
            return new DateOnly(date.Year, date.Month, date.Day);
        }
    }
}
