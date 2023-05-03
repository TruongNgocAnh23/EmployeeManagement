using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace MyApiNetCore6.Models
{
    public class Employee_Model
    {
        public int Employee_ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int Gender { get; set; }
        public string Pass { get; set; }
        public int Department_ID { get; set; }
        public bool Is_Manager { get; set; }
        public bool Is_Register { get; set; }
    }
    public class EmployeesList_Model
    {
        public int Employee_ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string Pass { get; set; }
        public int Department_ID { get; set; }
        public string Department_Name { get; set; }
        public bool Is_Manager { get; set; }
        public bool Is_Register { get; set; }
    }
}
