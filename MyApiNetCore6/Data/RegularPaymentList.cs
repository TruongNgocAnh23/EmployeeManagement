using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyApiNetCore6.Data
{
    public class RegularPaymentList
    {
        [Key]
        public int RegularPayment_ID { get; set; }
        public string Department_Name { get; set; }
        public decimal RegularPaymentPerHour { get; set; }
    }
}
