using MyApiNetCore6.Data;
using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee_Model>> GetAllEmployeesAsync();
        public Task<Employee_Model> GetAllEmployeesAsync(int Employee_ID);
    }
}
