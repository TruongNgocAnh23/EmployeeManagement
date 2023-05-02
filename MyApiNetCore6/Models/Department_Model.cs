using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace MyApiNetCore6.Models
{
    public class Department_Model
    {
        [JsonIgnore]
        public int Department_ID { get; set; }
        public string Department_Name { get; set; }

    }
}
