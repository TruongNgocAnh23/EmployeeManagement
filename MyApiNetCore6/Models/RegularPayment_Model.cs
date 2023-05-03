using System.ComponentModel.DataAnnotations;
namespace MyApiNetCore6.Models
{
    public class RegularPayment_Model
    {
        public int RegularPayment_ID { get; set; }
        public int Department_ID { get; set; }
        public decimal RegularPaymentPerHour { get; set; }
    }
    public class RegularPaymentList_Model
    {
        public int RegularPayment_ID { get; set; }
        public string Department_Name { get; set; }
        public decimal RegularPaymentPerHour { get; set; }
    }
}
