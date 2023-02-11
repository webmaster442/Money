using System.Reflection;
using System.Text;

namespace Money.Core
{
    public sealed class HtmlBuilder
    {
        private readonly StringBuilder _document;

        public HtmlBuilder(string documentTitle)
        {
            _document = new StringBuilder();
            _document.AppendLine("<!doctype html>");
            _document.AppendLine("<html lang=\"en\">");
            _document.AppendLine("<head>");
            _document.AppendLine("<meta charset=\"utf-8\">");
            _document.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
            _document.AppendFormat("<title>{0}</title>", documentTitle);
            _document.AppendLine("</head>");
            _document.AppendLine("<body>");
        }

        public HtmlBuilder Heading(int level, string text)
        {
            _document.AppendFormat("<h{0}>{1}</h{0}>\r\n", level, text);
            return this;
        }

        public HtmlBuilder Table<T>(IEnumerable<T> items) where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            _document.AppendLine("<table>");
            _document.AppendLine("<tr>");
            foreach (PropertyInfo property in properties) 
            {
                _document.AppendFormat("<th>{0}</th>\r\n", property.Name);
            }
            _document.AppendLine("</tr>");

            foreach (T? item in items)
            {
                string[] row = properties
                    .Select(x => x.GetValue(item)?.ToString() ?? "null")
                    .ToArray();

                _document.AppendLine("<tr>");
                foreach (var rowelement in row)
                {
                    _document.AppendFormat("<td>{0}</td>\r\n", rowelement);
                }
                _document.AppendLine("</tr>");
            }
            _document.AppendLine("</table>");

            return this;
        }

        public HtmlBuilder Table<TKey, TValue>(IDictionary<TKey, TValue> keyValuePairs)
        {
            _document.AppendLine("<table>");
            foreach (var keyValuePair in keyValuePairs) 
            {
                _document.AppendLine("<tr>");
                _document.AppendFormat("<td>{0}</td>\r\n", keyValuePair.Key);
                _document.AppendFormat("<td>{0}</td>\r\n", keyValuePair.Value);
                _document.AppendLine("</tr>");
            }
            _document.AppendLine("</table>");

            return this;
        }

        public string GetHtml()
        {
            _document.AppendLine("</body>");
            _document.AppendLine("</html>");
            return _document.ToString();
        }
    }
}
