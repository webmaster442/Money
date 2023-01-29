namespace Money.Data
{
    public sealed class Statistics
    {
        public int Count { get; set; }
        public required Dictionary<DateOnly, double> SumPerDay { get; set; }

        public int Days => SumPerDay.Count;

        public double Sum
            => SumPerDay.Sum(x => x.Value);

        public double AvgPerDay => Sum / Days;
        public double AvgPerCount => Sum / Count;
    }
}
