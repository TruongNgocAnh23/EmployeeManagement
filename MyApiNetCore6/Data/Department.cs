using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyApiNetCore6.Data
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public int Department_ID { get; set; }
        [MaxLength(100)]
        public string Department_Name { get; set; }
    }
}
