using Money.Data.Dto;

namespace Money.Commands
{
    internal sealed class StatCommand : AsyncCommand<StatSettings>
    {
        private readonly IReadonlyData _readonlyData;

        public StatCommand(IReadonlyData readonlyData)
        {
            _readonlyData = readonlyData;
        }

        public override async Task<int> ExecuteAsync(CommandContext context,
                                                     StatSettings settings)
        {
            Statistics stats = await _readonlyData.GetStatisticsAsync(settings.StartDate, settings.EndDate);

            Ui.BasicStats(stats, settings.StartDate, settings.EndDate);

            if (settings.Detailed)
            {
                Ui.DetailedStats(stats);
            }

            return Constants.Success;
        }
    }
}
