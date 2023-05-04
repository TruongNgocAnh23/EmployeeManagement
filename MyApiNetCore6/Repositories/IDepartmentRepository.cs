using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
namespace MyApiNetCore6.Repositories
{
    public interface IDepartmentRepository
    {
        public Task<List<Department_Model>> GetAllDepartmentsAsync();
        public Task<Department_Model> GetDepartmentAsync(int Department_ID);
        public Task<int> AddDepartmentAsync(Department_Model model);
        public Task<int> UpdateDepartmentAsync(Department_Model model);
        public Task<int> DeleteDepartmentAsync(int Department_ID);
    }
}
