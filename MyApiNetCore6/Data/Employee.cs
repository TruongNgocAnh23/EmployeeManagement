using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyApiNetCore6.Data
{
    public class Employee
    {
        [Key]
        public int Employee_ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int Gender { get; set; }
        public string Pass { get; set; }
        public int Department_ID { get; set; }
        public bool Is_Manager { get; set; }
        public bool Is_Register { get; set; }
    }
}
