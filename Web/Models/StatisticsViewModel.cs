namespace Money.Web.Models
{
    internal record class StatisticsViewModel
    {
        public required int SpendingCount { get; init; }
        public required SpendingViewModel Minimum { get; init; }
        public required SpendingViewModel Maximum { get; init; }
        public double AverageSpending => SumSpending / SpendingCount;
        public required double SumSpending { get; init; }
        public required Dictionary<DateOnly, double> SpendigsPerDay { get; init; }

        public DateOnly DayWithMostSpendings => SpendigsPerDay.MaxBy(s => s.Value).Key;
        public DateOnly DayWithLeastSpendings => SpendigsPerDay.MinBy(s => s.Value).Key;
    }
}
