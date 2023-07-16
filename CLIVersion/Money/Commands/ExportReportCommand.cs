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
        settings.AppendHtmlToFileNameWhenNeeded();

        Data.Dto.Statistics stats = await _readonlyData.GetStatisticsAsync(settings.StartDate!.Value,
                                                           settings.EndDate!.Value);
        var data = await _readonlyData
            .Find(string.Empty, string.Empty, settings.StartDate, settings.EndDate, false)
            .ToListAsync();

        string html = ReportFactory.CreateReport(data,
                                                 stats,
                                                 settings.StartDate!.Value,
                                                 settings.EndDate!.Value);
        try
        {
            File.WriteAllText(settings.FileName, html);
            Ui.Success(Resources.SuccessReport, settings.FileName);
        }
        catch (IOException ex)
        {
            Ui.PrintException(ex);
            return Constants.IoError;
        }

        return Constants.Success;
    }
}
