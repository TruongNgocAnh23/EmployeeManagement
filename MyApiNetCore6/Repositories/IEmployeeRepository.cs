using MyApiNetCore6.Data;
using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<List<EmployeesList_Model>> GetAllEmployeesAsync(int? Department_ID, int? Is_Register);
        public Task<EmployeesList_Model> GetEmployeesAsync(int Employee_ID);
        public Task<int> RegisterAsync(Employee_Model model);
        public Task<int> UpdateEmployeeeAsync(Employee_Model model);
        public Task<int> DeleteEmployeeAsync(int Employee_ID);
    }
}
