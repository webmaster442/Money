using System.ComponentModel;
using System.Globalization;

namespace Money.Converters;

internal sealed class NullableDateonlyConverter : TypeConverter
{
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
                return null;

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
