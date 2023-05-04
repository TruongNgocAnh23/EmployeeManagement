namespace MyApiNetCore6.Models
{
    public class Manager_Model
    {

    }
    public class GetListTimesheet_Model
    {
        public int Employee_ID { get; set; }
        public string Name { get; set;}
        public int Department_ID { get; set; }
        public string Department_Name { get; set;}
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
    }
    public class AddTimesheet_Model
    {
        public int Manager_ID { get; set; }
        public int Employee_ID { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
    }
    public class UpdateTimesheet_Model
    {
        public int Manager_ID { get; set; }
        public int Timesheet_ID { get; set; }
        public int Employee_ID { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
    }
    public class DeleteTimesheet_Model
    {
        public int Manager_ID { get; set; }
        public int Timesheet_ID { get; set; }
    }
}
