using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Money.Data.Dto
{
    public class SerializableSpending
    {
        public DateOnly Date { get; set; }

        public string Description { get; set; }

        public double Ammount { get; set; }

        public DateTime AddedOn { get; set; }

        public SerializableSpending()
        {
            Description = string.Empty;
        }
    }
}
