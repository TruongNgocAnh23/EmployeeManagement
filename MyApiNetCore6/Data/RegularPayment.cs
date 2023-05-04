using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyApiNetCore6.Data
{
    public class RegularPayment
    {
        [Key]
        public int RegularPayment_ID { get; set; }
        [MaxLength(100)]
        public int Department_ID { get; set; }
        public decimal RegularPaymentPerHour { get; set; }
    }
}
