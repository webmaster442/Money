using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Money.Extensions
{
    internal static class StreamExtensions
    {
        private readonly static JsonSerializerOptions Settings =
            new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            };

        public static void WriteJson<T>(this Stream stream, T value)
        {
            JsonSerializer.Serialize(stream, value, Settings);
        }
    }
}
