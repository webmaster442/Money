using Money.Core;
using Money.Data.Dto;

namespace Money.Infrastructure;
internal static class ReportFactory
{
    public static string CreateReport(IEnumerable<UiDataRow> data, Statistics statistics)
    {
        HtmlBuilder html = new("Report");

        var dates = data.GroupBy(x => x.Date);

        foreach (var date in dates)
        {
            html
                .Heading(2, $"{date.Key} - {date.Select(x => x.Ammount).Sum()}")
                .Table(date);
        }

        return html.GetHtml();
    }
}
