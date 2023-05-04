namespace MyApiNetCore6.Models
{
    public class TimeSheet_Model
    {
        public int TimeSheet_ID { get; set; }
        public int Employee_ID { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

    }
    public class CheckIn_Model
    {
        public int Employee_ID { get; set; }
        public DateTime? CheckIn { get; set; }
    }
    public class CheckOut_Model
    {
        public int TimeSheet_ID { get; set; }
        public DateTime? CheckOut { get; set; }
    }
}
