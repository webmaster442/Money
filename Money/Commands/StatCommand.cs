using System.Diagnostics.CodeAnalysis;

namespace Money.Commands
{
    internal sealed class StatCommand : Command<StatSettings>
    {
        private readonly IReadonlyData _readonlyData;

        public StatCommand(IReadonlyData readonlyData)
        {
            _readonlyData = readonlyData;
        }

        public override int Execute([NotNull] CommandContext context,
                                    [NotNull] StatSettings settings)
        {
            Data.Dto.Statistics stats = _readonlyData.GetStatisticsAsync(settings.StartDate, settings.EndDate);

            Ui.BasicStats(stats, settings.StartDate, settings.EndDate);

            if (settings.Detailed)
            {
                Ui.DetailedStats(stats);
            }

            return Constants.Success;
        }
    }
}
