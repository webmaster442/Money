﻿using System.ComponentModel;
using System.Globalization;

namespace Money.CommandsSettings
{
    internal sealed class DateonlyConverter : TypeConverter
    {
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string stringValue)
            {
                if (DateOnly.TryParse(stringValue, out var parsed))
                {
                    return parsed;
                }
                else
                {
                    throw new InvalidOperationException($"{stringValue} is not a valid date");
                }
            }
            throw new NotSupportedException("Can't convert value to DateOnly.");
        }
    }
}