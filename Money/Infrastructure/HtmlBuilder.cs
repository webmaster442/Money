using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Text;

namespace Money.Core
{
    public sealed class HtmlBuilder
    {
        private readonly StringBuilder _document;
        private readonly CultureInfo _culture;

        public HtmlBuilder(string documentTitle, CultureInfo culture)
        {
            _culture = culture;
            _document = new StringBuilder(16*1024);
            _document
                .AppendLine("<!doctype html>")
                .AppendFormat("<html lang=\"{0}\">\r\n", _culture.TwoLetterISOLanguageName)
                .AppendLine("<head>")
                .AppendLine("<meta charset=\"utf-8\">")
                .AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">")
                .AppendFormat("<title>{0}</title>\r\n", documentTitle)
                .AppendLine("<style>")
                .AppendLine(Resources.style)
                .AppendLine("</style>")
                .AppendLine("</head>")
                .AppendLine("<body>")
                .AppendLine("<div id=\"container\">");
        }

        private static bool IsNumber(object? value)
        {
            if (value == null)
                return false;

            Type objType = value.GetType();
            objType = Nullable.GetUnderlyingType(objType) ?? objType;

            return objType == typeof(int)
                || objType == typeof(long)
                || objType == typeof(float)
                || objType == typeof(double);
        }

        private static string ToString(object? obj, CultureInfo culture)
        {
            string? format = IsNumber(obj) ? "N0" : null;

            return obj switch
            {
                IFormattable formattable => formattable.ToString(format, culture),
                IConvertible convertible => convertible.ToString(culture),
                _ => obj?.ToString() ?? "null"
            };
        }

        public HtmlBuilder Details(string text, Action<HtmlBuilder> contentGenerator)
        {
            _document
                .AppendLine("<details>")
                .AppendFormat("<summary>{0}</summary>\r\n", text);
            contentGenerator.Invoke(this);
            _document.AppendLine("</details>");
            return this;
        }

        public HtmlBuilder Heading(int level, string text)
        {
            _document.AppendFormat("<h{0}>{1}</h{0}>\r\n", level, text);
            return this;
        }

        public HtmlBuilder Table<T>(IEnumerable<T> items) where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            _document
                .AppendLine("<div style=\"overflow-x: auto;\">")
                .AppendLine("<table>")
                .AppendLine("<tr>");
            foreach (PropertyInfo property in properties)
            {
                _document.AppendFormat("<th>{0}</th>\r\n", property.Name);
            }
            _document.AppendLine("</tr>");

            foreach (T? item in items)
            {
                string[] row = properties
                    .Select(x => ToString(x.GetValue(item), _culture))
                    .ToArray();

                _document.AppendLine("<tr>");
                foreach (var rowelement in row)
                {
                    _document.AppendFormat("<td>{0}</td>\r\n", rowelement);
                }
                _document.AppendLine("</tr>");
            }
            _document
                .AppendLine("</table>")
                .AppendLine("</div>");

            return this;
        }

        public HtmlBuilder Table<TKey, TValue>(IDictionary<TKey, TValue> keyValuePairs)
        {
            _document
                .AppendLine("<div style=\"overflow-x: auto;\">")
                .AppendLine("<table>");
            foreach (var keyValuePair in keyValuePairs)
            {
                _document
                    .AppendLine("<tr>")
                    .AppendFormat("<td>{0}</td>\r\n", ToString(keyValuePair.Key, _culture))
                    .AppendFormat("<td>{0}</td>\r\n", ToString(keyValuePair.Value, _culture))
                    .AppendLine("</tr>");
            }
            _document
                .AppendLine("</table>")
                .AppendLine("</div>");

            return this;
        }

        public string GetHtml()
        {
            _document
                .AppendLine("</div>")
                .AppendLine("</body>")
                .AppendLine("</html>");
            return _document.ToString();
        }
    }
}
