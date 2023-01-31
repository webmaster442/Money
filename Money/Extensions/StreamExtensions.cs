using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

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

        public static T ReadJson<T>(this Stream stream)
        {
            return JsonSerializer.Deserialize<T>(stream, Settings)
                ?? throw new InvalidDataException(Resources.ErrorInvalidJson);
        }
    }
}
