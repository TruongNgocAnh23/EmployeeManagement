using MyApiNetCore6.Data;
using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<List<EmployeesList_Model>> GetAllEmployeesAsync(int? Department_ID, int? Is_Register);
        public Task<EmployeesList_Model> GetEmployeesAsync(int Employee_ID);
        public Task<int> RegisterAsync(Employee_Model model);
        public Task<int> UpdateEmployeeeAsync(ChangePassword_Model model);
        public Task<int> DeleteEmployeeAsync(int Employee_ID);
        public Task<int> CheckInAsync(CheckIn_Model model);
        public Task<int> CheckOutAsync(CheckOut_Model model);
        public Task<int> SetManagerAsync(SetManager_Model model);

    }
}
