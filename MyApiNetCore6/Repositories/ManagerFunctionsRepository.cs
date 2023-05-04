using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using System.Text.Json.Serialization;
using System.Linq;

namespace MyApiNetCore6.Repositories
{
    public class ManagerFunctionsRepository : IManagerFunctionsRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;
        public ManagerFunctionsRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<GetListTimesheet_Model>> GetListTimeSheetAsync(int? Manager_ID, int? Year, int? Month)
        {
            var CheckManager = _context.Employee.SingleOrDefault(p => p.Employee_ID == Manager_ID & p.Is_Manager == true);
            if (CheckManager != null)
            {
                var ListTimeSheet = await (from em in _context.Employee
                                   join d in _context.Departments on em.Department_ID equals d.Department_ID
                                   join t in _context.Timesheet! on em.Employee_ID equals t.Employee_ID
                                   where em.Department_ID == CheckManager.Department_ID
                                   & t.CheckIn.Value.Year == Year
                                   & t.CheckIn.Value.Month == Month
                                   select new GetListTimesheet_Model
                                   {
                                       Employee_ID = em.Employee_ID,
                                       Name = em.Name,
                                       Department_ID = em.Department_ID,
                                       Department_Name = d.Department_Name,
                                       CheckIn = t.CheckIn,
                                       CheckOut = t.CheckOut
                                   }).ToListAsync();
                return ListTimeSheet;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> AddTimesheetAsync(AddTimesheet_Model model)
        {
            var CheckManager = _context.Employee.SingleOrDefault(p => p.Employee_ID == model.Manager_ID & p.Is_Manager == true);
            if (CheckManager == null)
            {
                return 5;
            }
            if (model.CheckIn.ToString() == "" || model.CheckIn == null)
            {
                return 3;
            }
            var CheckEm = _context.Employee.SingleOrDefault(p => p.Employee_ID == model.Employee_ID & p.Department_ID == CheckManager.Department_ID);
            if (CheckEm != null)
            {
                var CheckTime = _context.Timesheet.SingleOrDefault(p => p.CheckIn.Value.Day == model.CheckIn.Value.Day & p.CheckIn.Value.Month == model.CheckIn.Value.Month & p.CheckIn.Value.Year == model.CheckIn.Value.Year & p.Employee_ID == model.Employee_ID);
                if (CheckTime == null)
                {
                    var Check = new TimeSheet();
                    Check.Employee_ID = model.Employee_ID;
                    Check.CheckIn = model.CheckIn;
                    Check.CheckOut = model.CheckOut;
                    Check.CreateBy = model.Manager_ID;
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

        public async Task<int> UpdateTimesheetAsync(UpdateTimesheet_Model model)
        {
            var CheckManager = _context.Employee.SingleOrDefault(p => p.Employee_ID == model.Manager_ID & p.Is_Manager == true);
            if (CheckManager == null)
            {
                return 5;
            }
            if (model.CheckIn.ToString() == "" || model.CheckIn == null)
            {
                return 3;
            }
            
            var CheckEm = _context.Employee.SingleOrDefault(p => p.Employee_ID == model.Employee_ID & p.Department_ID == CheckManager.Department_ID);
            if (CheckEm != null)
            {
                var CheckTime = _context.Timesheet.SingleOrDefault(p => p.TimeSheet_ID == model.Timesheet_ID);
                if (CheckTime != null)
                {
                    CheckTime.Employee_ID = model.Employee_ID;
                    CheckTime.CheckIn = model.CheckIn;
                    CheckTime.CheckOut = model.CheckOut;
                    CheckTime.UpdateBy = model.Manager_ID;
                    CheckTime.UpdateDate = DateTime.Now;
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
        public async Task<int> DeleteTimesheetAsync(DeleteTimesheet_Model model)
        {
            var CheckManager = _context.Employee.SingleOrDefault(p => p.Employee_ID == model.Manager_ID & p.Is_Manager == true);
            if (CheckManager == null)
            {
                return 2;
            }
            var CheckTime = _context.Timesheet.SingleOrDefault(p => p.TimeSheet_ID == model.Timesheet_ID);
            if (CheckTime != null)
            {
                var CheckEm = _context.Employee.SingleOrDefault(p => p.Employee_ID == CheckTime.Employee_ID & p.Department_ID == CheckManager.Department_ID);
                if (CheckEm != null)
                {
                    _context.Timesheet.Remove(CheckTime);
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
                return 1;
            }
        }
    }
}
