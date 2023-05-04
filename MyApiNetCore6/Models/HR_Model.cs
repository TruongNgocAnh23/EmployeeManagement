namespace MyApiNetCore6.Models
{
    public class HR_Model
    {
        
    }
    public class HRGetListTimesheet_Model
    {
        public int Employee_ID { get; set; }
        public string Name { get; set; }
        public int Department_ID { get; set; }
        public string Department_Name { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
    }
    public class HighestWorkingHourOfEmployee_Model
    {
        public int Employee_ID { get; set; }
        public string Name { get; set; }
        public int Department_ID { get; set; }
        public string Department_Name { get; set; }
        public int WorkingHour { get; set; }
    }
    public class CaculateSalary_Model
    {
        public int Employee_ID { get; set; }
        public string Name { get; set; }
        public int Department_ID { get; set; }
        public string Department_Name { get; set; }
        public int WorkingHour { get; set; }
        public decimal? Salary { get; set; }
    }
}
