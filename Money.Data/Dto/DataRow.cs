using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Money.Data.Entities;

namespace Money.Data.Dto
{
    public sealed class DataRow
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Ammount { get; set; }
        public DateTime AddedOn { get; set; }
        public string CategoryName { get; set; }

        internal DataRow(Spending spending)
        {
            Date = spending.Date.ToDateTime(TimeOnly.MinValue);
            Description = spending.Description;
            AddedOn = spending.AddedOn;
            Ammount = spending.Ammount;
            CategoryName = spending.Category.Description;
        }

        public DataRow()
        {
            Description = string.Empty;
            CategoryName = string.Empty;
        }

        public static DataRow Parse(string @string)
        {
            string[] parts = @string.Split(';', StringSplitOptions.RemoveEmptyEntries);
            return new DataRow
            {
                Date = DateTime.Parse(parts[0], CultureInfo.InvariantCulture),
                Description = parts[1],
                Ammount = double.Parse(parts[2], CultureInfo.InvariantCulture),
                AddedOn = DateTime.Parse(parts[3], CultureInfo.InvariantCulture),
                CategoryName = parts[4]
            };
        }

        public string ToCsvRow()
        {
            var c = CultureInfo.InvariantCulture;
            return $"{Date.ToString(c)};{Description};{Ammount.ToString(c)};{AddedOn.ToString(c)};{CategoryName}";
        }
    }
}
