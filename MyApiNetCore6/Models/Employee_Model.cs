using System.ComponentModel.DataAnnotations;
namespace MyApiNetCore6.Models
{
    public class Employee_Model
    {
        public int Employee_ID { get; set; }
        public string Name { get; set; }
        [Range(0,400)]
        public string UserName { get; set; }
        [Range(0, 400)]
        public string Pass { get; set; }
        public string Department { get; set; }
        public bool Is_Manager { get; set; }
        public string Gender { get; set; }
    }
}
