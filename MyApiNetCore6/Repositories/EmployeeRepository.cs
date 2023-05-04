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
            if (model.Gender > 3 || model.Gender < 0)
            {
                return 5;
            }
            var CheckDepartment = _context.Departments.SingleOrDefault(p => p.Department_ID == model.Department_ID);
            if (CheckDepartment == null)
            {
                return 6;
            }    
            var CheckEm = _context.Employee.SingleOrDefault(p => p.Department_ID == model.Department_ID & p.Name == model.Name);
            if (CheckEm == null)
            {
                // Password length > 6 characters?
                if (model.Pass.Length >= 6)
                {
                    //Password have at least  a number character?
                    var b = String.Empty;
                    for (int i = 0; i < model.Pass.Length; i++)
                    {
                        if (Char.IsDigit(model.Pass[i]))
                            b += model.Pass[i];
                    }
                    if (b.Length > 0)
                    {
                        //Password have at least a special character?
                        var withoutSpecial = new string(model.Pass.Where(c => Char.IsLetterOrDigit(c)
                                            || Char.IsWhiteSpace(c)).ToArray());
                        if (model.Pass != withoutSpecial)
                        {
                            var newEm = _mapper.Map<Employee>(model);
                            _context.Employee.Add(newEm);
                            await _context.SaveChangesAsync();
                            return 0;
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
                return 4;
            }
        }

        public async Task<int> UpdateEmployeeeAsync(ChangePassword_Model model)
        {
            var CheckEm = _context.Employee.SingleOrDefault(p => p.Employee_ID == model.Employee_ID);
            if (CheckEm != null)
            {
                if (model.Pass.Length >= 6)
                {
                    //Password have at least  a number character?
                    var b = String.Empty;
                    for (int i = 0; i < model.Pass.Length; i++)
                    {
                        if (Char.IsDigit(model.Pass[i]))
                            b += model.Pass[i];
                    }
                    if (b.Length > 0)
                    {
                        //Password have at least a special character?
                        var withoutSpecial = new string(model.Pass.Where(c => Char.IsLetterOrDigit(c)
                                            || Char.IsWhiteSpace(c)).ToArray());
                        if (model.Pass != withoutSpecial)
                        {
                            CheckEm.Pass = model.Pass;
                            await _context.SaveChangesAsync();
                            return 0;
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
                return 4;
            }    
        }

        public async Task<int> CheckInAsync(CheckIn_Model model)
        {
            if (model.CheckIn.ToString() == "" || model.CheckIn == null)
            {
                return 3;
            }    
            var CheckEm = _context.Employee.SingleOrDefault(p => p.Employee_ID == model.Employee_ID);
            if (CheckEm != null)
            {
                var CheckTime = _context.Timesheet.SingleOrDefault(p => p.CheckIn.Value.Day == model.CheckIn.Value.Day & p.CheckIn.Value.Month == model.CheckIn.Value.Month & p.CheckIn.Value.Year == model.CheckIn.Value.Year & p.Employee_ID == model.Employee_ID);
                if (CheckTime == null)
                {
                    var Check = new TimeSheet();
                    Check.Employee_ID = model.Employee_ID;
                    Check.CheckIn = model.CheckIn;
                    Check.CreateBy = model.Employee_ID;
                    Check.CreateDate = DateTime.Now;
                    _context.Timesheet!.Add(Check);
                    await _context.SaveChangesAsync();
                    return 0;
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
        public async Task<int> CheckOutAsync(CheckOut_Model model)
        {
            if (model.CheckOut.ToString() == "" || model.CheckOut == null)
            {
                return 2;
            }
            var CheckEm = _context.Timesheet.SingleOrDefault(p => p.TimeSheet_ID == model.TimeSheet_ID);
            if (CheckEm != null)
            {
                CheckEm.CheckOut = model.CheckOut;
                await _context.SaveChangesAsync();
                return 0;
            }
            else
            {
                return 1;
            }
        }
        public async Task<int> SetManagerAsync(SetManager_Model model)
        {
            var CheckEm = _context.Employee.SingleOrDefault(p => p.Employee_ID == model.Employee_ID);
            if (CheckEm != null)
            {
                CheckEm.Is_Manager = model.Is_Manager;
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
