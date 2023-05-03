using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using System.Text.Json.Serialization;
using System.Linq;

namespace MyApiNetCore6.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;
        private const int Result = 0;
        public EmployeeRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<int> DeleteEmployeeAsync(int Employee_ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EmployeesList_Model>> GetAllEmployeesAsync(int? Department_ID, int? Is_Register)
        {
            var ListE = await (from em in _context.Employee
                               join d in _context.Departments! on em.Department_ID equals d.Department_ID
                               select new EmployeesList_Model
                               {
                                   Employee_ID = em.Employee_ID,
                                   Name = em.Name,
                                   UserName = em.UserName,
                                   Gender = em.Gender == 1 ? "Male" : em.Gender == 2 ? "Female" : "Other",
                                   Pass = em.Pass,
                                   Department_ID = d.Department_ID,
                                   Department_Name = d.Department_Name,
                                   Is_Manager = em.Is_Manager!,
                                   Is_Register = em.Is_Register,
                                  
                               }).ToListAsync();
            if (ListE.Count() > 0)
            {
                if (Department_ID == 0 & Is_Register == 0)
                {
                    var Em = ListE;
                    return Em;
                }
                else if (Department_ID == 0 &  Is_Register == 1)
                {
                    var Em = ListE.Where(p => p.Is_Register == true).ToList();
                    return Em;
                }
                else if (Department_ID == 0 & Is_Register > 1)
                {
                    var Em = ListE.Where(p => p.Is_Register == false).ToList();
                    return Em;
                }
                else if (Department_ID == 1 & Is_Register == 0)
                {
                    var Em = ListE.Where(p => p.Department_ID == Department_ID).ToList();
                    return Em;
                }
                else if (Department_ID == 1 & Is_Register == 1)
                {
                    var Em = ListE.Where(p => p.Department_ID == Department_ID & p.Is_Register == true).ToList();
                    return Em;
                }
                else
                {
                    var Em = ListE.Where(p => p.Department_ID == Department_ID & p.Is_Register == false).ToList();
                    return Em;
                }    

            }  
            else
            {
                return ListE;
            }    
            
        }

        public async Task<EmployeesList_Model> GetEmployeesAsync(int Employee_ID)
        {
            var ListE = await (from em in _context.Employee
                               join d in _context.Departments! on em.Department_ID equals d.Department_ID
                               where em.Employee_ID == Employee_ID
                               select new EmployeesList_Model
                               {
                                   Employee_ID = em.Employee_ID,
                                   Name = em.Name,
                                   UserName = em.UserName,
                                   Gender = em.Gender == 1 ? "Male" : em.Gender == 2 ? "Female" : "Other",
                                   Pass = em.Pass,
                                   Department_ID = d.Department_ID,
                                   Department_Name = d.Department_Name,
                                   Is_Manager = em.Is_Manager!,
                                   Is_Register = em.Is_Register,

                               }).ToListAsync();
            return ListE.FirstOrDefault()!;
        }

        public async Task<int> RegisterAsync(Employee_Model model)
        {
            var CheckEm = _context.Employee.SingleOrDefault(p => p.Department_ID == model.Department_ID & p.Name == model.Name);
            if (CheckEm == null)
            {
                if (model.Pass.Length >= 6)
                {
                    var b = String.Empty;
                    for (int i = 0; i < model.Pass.Length; i++)
                    {
                        if (Char.IsDigit(model.Pass[i]))
                            b += model.Pass[i];
                    }
                    if (b.Length > 0)
                    {
                        var withoutSpecial = new string(model.Pass.Where(c => Char.IsLetterOrDigit(c)
                                            || Char.IsWhiteSpace(c)).ToArray());
                        if (model.Pass != withoutSpecial)
                        {
                            var newEm = _mapper.Map<Employee>(model);
                            _context.Employee.Add(newEm);
                            await _context.SaveChangesAsync();
                            return newEm.Employee_ID;
                        }
                        else
                        {
                            return 3;

                        }    
                      
                    }
                    else
                    {
                        return 2;
                    }
                }   
                else
                {
                    return 1;
                }    
               
                
            }
            else
            {
                return Result;
            }
        }

        public Task<int> UpdateEmployeeeAsync(Employee_Model model)
        {
            throw new NotImplementedException();
        }
    }
}
