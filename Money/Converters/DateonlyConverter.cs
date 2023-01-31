using System.ComponentModel;
using System.Globalization;

namespace Money.Converters
{
    internal sealed class DateonlyConverter : TypeConverter
    {
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string stringValue)
            {
                if (DateOnly.TryParse(stringValue, out DateOnly parsed))
                {
                    return parsed;
                }
                else
                {
                    throw new InvalidOperationException($"{stringValue} {Resources.ErrorNotValidDate}");
                }
            }
            throw new NotSupportedException(Resources.ErrorCantConvertDateOnly);
        }
    }
}