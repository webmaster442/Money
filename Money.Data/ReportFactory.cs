using Money.Core;
using Money.Data.Dto;

namespace Money.Data;
public static class ReportFactory
{
    public static string CreateReport(Statistics statistics, IList<UiDataRow> spendings)
    {
        HtmlBuilder html = new("Report");
        html
            .Heading(1, "Stats")
            .Heading(2, "Sum/day")
            .Table(statistics.SumPerDay)
            .Heading(2, "Sum/cat")
            .Table(statistics.SumPerCategory)
            .Heading(1, "Spendings")
            .Table(spendings);

        return html.GetHtml();
    }
}
