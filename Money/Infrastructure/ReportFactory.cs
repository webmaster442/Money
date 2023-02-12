using System.Globalization;
using System.Net;

using Money.Core;
using Money.Data.Dto;

namespace Money.Infrastructure;
internal static class ReportFactory
{
    private static string HeaderStatistics(IEnumerable<UiDataRow> items, double total)
    {
        double sum = 0;
        double min = double.MaxValue;
        double max = double.MinValue;
        int count = 0;

        foreach (var item in items) 
        {
            ++count;
            sum += item.Ammount;
            if (item.Ammount < min) 
                min = item.Ammount;
            if (item.Ammount > max)
                max = item.Ammount;
        }

        double avg = sum / count;
        double percent = sum / total;


        return $"<pre>" +
            $"<p>Sum: {sum:C0}" +
            $"<br>Min: {min:C0}" +
            $"<br>Max: {max:C0}" +
            $"<br>Avg: {avg:C0}" +
            $"<br>%: {percent:P0}" +
            $"</p></pre>";
    }

    public static string CreateReport(IEnumerable<UiDataRow> data, 
                                      Statistics statistics,
                                      DateOnly startDate,
                                      DateOnly endDate)
    {
        HtmlBuilder html = new("Report", CultureInfo.CurrentUICulture);

        var dates = data.GroupBy(x => x.Date);

        html.Heading(1, string.Format(Resources.StatFromToHeader, startDate, endDate));

        Dictionary<string, string> statTable = new()
        {
            { Resources.StatSpendingCount, statistics.Count.ToString("N0") },
            { Resources.StatSpendingWithDays, statistics.Days.ToString("N0") },
            { Resources.StatTotalSpending, statistics.Sum.ToString("C0") },
            { Resources.StatAveragePerCount, statistics.AvgPerCount.ToString("C0") },
            { Resources.StatAveragePerDay, statistics.AvgPerDay.ToString("C0") },
        };

        foreach (var item in statistics.SumPerCategory)
        {
            statTable.Add(item.Key, item.Value.ToString("C0"));
        }

        html.Table(statTable);

        html.Heading(1, "Details");

        foreach (var date in dates)
        {
            html.Details($"{date.Key} {HeaderStatistics(date, statistics.Sum)}",
                         (content) => content.Table(date));
        }

        return html.GetHtml();
    }
}
