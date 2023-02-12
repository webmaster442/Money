using System.Globalization;

using Money.Core;
using Money.Data.Dto;

namespace Money.Infrastructure;
internal static class ReportFactory
{
    public static string CreateReport(IEnumerable<UiDataRow> data, Statistics statistics)
    {
        HtmlBuilder html = new("Report", CultureInfo.CurrentUICulture);

        var dates = data.GroupBy(x => x.Date);

        foreach (var date in dates)
        {
            html.Details($"{date.Key} - {date.Select(x => x.Ammount).Sum()}",
                         (content) => content.Table(date));
        }

        return html.GetHtml();
    }
}
