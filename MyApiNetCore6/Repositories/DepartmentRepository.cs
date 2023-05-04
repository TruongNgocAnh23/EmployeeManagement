using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using System.Text.Json.Serialization;
using System.Linq;
namespace MyApiNetCore6.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;
        public DepartmentRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddDepartmentAsync(Department_Model model)
        {
            var Check = _context.Departments.SingleOrDefault(p => p.Department_ID == model.Department_ID);
            if (Check == null)
            {
                var newDepartment = _mapper.Map<Department>(model);
                _context.Departments!.Add(newDepartment);
                await _context.SaveChangesAsync();
                return 0;
            }    
            else
            {
                return 1;
            }    
           
        }

        public async Task<int> DeleteDepartmentAsync(int Department_ID)
        {
            var deleteDepartment = _context.Departments!.SingleOrDefault(p => p.Department_ID == Department_ID);
            if (deleteDepartment != null)
            {
                _context.Departments.Remove(deleteDepartment);
                await _context.SaveChangesAsync();
                return 0;
            }  
            else
            {
                return 1;
            }    
        }

        public async Task<List<Department_Model>> GetAllDepartmentsAsync()
        {
            var departments = await _context.Departments!.ToListAsync();
            return _mapper.Map<List<Department_Model>>(departments);
        }

        public  async Task<Department_Model> GetDepartmentAsync(int Department_ID)
        {
            var department = await _context.Departments!.FindAsync(Department_ID);
            return _mapper.Map<Department_Model>(department);
        }

        public async Task<int> UpdateDepartmentAsync(Department_Model model)
        {
            var check = _context.Departments.FirstOrDefault(p => p.Department_ID == model.Department_ID);
            if (check != null)
            {
                var a = 
                check.Department_Name = model.Department_Name;
                await _context.SaveChangesAsync();
                return 0;
            }
            else
            {
                return 1;
            }    
        }
       
    }
}
