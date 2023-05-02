using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
namespace MyApiNetCore6.Repositories
{
    public interface IDepartmentRepository
    {
        public Task<List<Department_Model>> GetAllDepartmentsAsync();
        public Task<Department_Model> GetDepartmentAsync(int Department_ID);
        public Task<int> AddDepartmentAsync(Department_Model model);
        public Task UpdateDepartmentAsync(Department_Model model);
        public Task DeleteDepartmentAsync(int Department_ID);
    }
}
