using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money.Commands;
internal class ExportReportCommand : AsyncCommand<ExportSetting>
{
    private readonly IReadonlyData _readonlyData;

    public ExportReportCommand(IReadonlyData readonlyData)
    {
        _readonlyData = readonlyData;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, ExportSetting settings)
    {
        try
        {
            var stats = await _readonlyData.GetStatisticsAsync(settings.StartDate.Value, settings.EndDate.Value);
            var data = await _readonlyData.Find("", "", settings.StartDate, settings.EndDate, false);

            var html = ReportFactory.CreateReport(stats, data);

        }
        catch (IOException)
        {
            return Constants.IoError;
        }

        return Constants.Success;
    }
}
