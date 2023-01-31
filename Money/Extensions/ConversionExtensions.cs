using System.Globalization;

namespace Money.Extensions
{
    internal static class ConversionExtensions
    {
        public static DateOnly ToDateOnly(this object value)
        {
            if (value is DateTime dt)
                return dt.ToDateOnly();

            if (value is DateOnly d)
                return d;

            if (value is string str)
                return DateOnly.Parse(str);

            throw new InvalidDataException($"Can't convert {value.GetType().FullName} to {nameof(DateOnly)}");
        }

        public static double ToDouble(this object value)
        {
            if (value is IConvertible convertible)
                return convertible.ToDouble(CultureInfo.CurrentCulture);

            throw new InvalidDataException($"Can't convert {value.GetType().FullName} to {nameof(Double)}");
        }

        public static DateTime ToDateTime(this object value, DateTime defaultValue)
        {
            if (value == null || value is DBNull)
                return defaultValue;

            if (value is DateTime dt)
                return dt;

            if (value is string str)
                return DateTime.Parse(str);

            throw new InvalidDataException($"Can't convert {value.GetType().FullName} to {nameof(DateTime)}");
        }

    }
}
