using Money.Infrastructure;

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
        settings.EnsureHasDate();

        var stats = await _readonlyData.GetStatisticsAsync(settings.StartDate!.Value, settings.EndDate!.Value);
        var data = await _readonlyData.Find("", "", settings.StartDate, settings.EndDate, false);

        var html = ReportFactory.CreateReport(data, stats);
        try
        {
            File.WriteAllText(settings.FileName, html);
        }
        catch (IOException ex)
        {
            Ui.PrintException(ex);
            return Constants.IoError;
        }

        return Constants.Success;
    }
}
