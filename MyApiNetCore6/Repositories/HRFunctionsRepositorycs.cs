using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using System.Text.Json.Serialization;
using System.Linq;

namespace MyApiNetCore6.Repositories
{
    public class HRFunctionsRepositorycs : IHRFunctionsRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;
        public HRFunctionsRepositorycs(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<HRGetListTimesheet_Model>> HRGetListTimeSheetAsync(int? HR_ID,int? Department_ID, int? Year, int? Month)
        {
            var CheckManager = (from em in _context.Employee
                                join d in _context.Departments on em.Department_ID equals d.Department_ID
                                where
                                em.Employee_ID == HR_ID
                                & d.Department_Name.ToUpper().Contains("HR")
                                select new
                                {
                                   em.Employee_ID
                                }).SingleOrDefault();
            if (CheckManager != null)
            {
                if (Department_ID == 0)
                {
                    var ListTimeSheet = await (from em in _context.Employee
                                               join d in _context.Departments on em.Department_ID equals d.Department_ID
                                               join t in _context.Timesheet! on em.Employee_ID equals t.Employee_ID
                                               where 
                                               t.CheckIn.Value.Year == Year
                                               & t.CheckIn.Value.Month == Month
                                               select new HRGetListTimesheet_Model
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
                    var ListTimeSheet = await (from em in _context.Employee
                                               join d in _context.Departments on em.Department_ID equals d.Department_ID
                                               join t in _context.Timesheet! on em.Employee_ID equals t.Employee_ID
                                               where
                                               d.Department_ID == Department_ID
                                               & t.CheckIn.Value.Year == Year
                                               & t.CheckIn.Value.Month == Month
                                               select new HRGetListTimesheet_Model
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
               
            }
            else
            {
                return null;
            }
        }
        public async Task<HighestWorkingHourOfEmployee_Model> GetHighestWorkingHourOfEmployee(int? HR_ID, int? Department_ID, int? Year, int? Month)
        {
            var CheckManager = (from em in _context.Employee
                                join d in _context.Departments on em.Department_ID equals d.Department_ID
                                where
                                em.Employee_ID == HR_ID
                                & d.Department_Name.ToUpper().Contains("HR")
                                select new
                                {
                                    em.Employee_ID
                                }).SingleOrDefault();
            if (CheckManager != null)
            {
                if (Department_ID == 0)
                {
                    var ListTimesheet = await (from em in _context.Employee
                                               select new HighestWorkingHourOfEmployee_Model
                                               {
                                                   Employee_ID = em.Employee_ID,
                                                   Name = em.Name,
                                                   Department_ID = _context.Departments.FirstOrDefault(p=>p.Department_ID == em.Department_ID).Department_ID,
                                                   Department_Name = _context.Departments.FirstOrDefault(p => p.Department_ID == em.Department_ID).Department_Name,
                                                   WorkingHour = _context.Timesheet.Where(p=>p.Employee_ID == em.Employee_ID & p.CheckIn != null & p.CheckIn.Value.Month == Month & p.CheckIn.Value.Year == Year & p.CheckOut != null).Sum(o=>o.CheckOut.Value.Hour - o.CheckIn.Value.Hour)
                                               }).OrderByDescending(o=>o.WorkingHour).ToListAsync();
                    if (ListTimesheet.Count() > 0)
                    {
                        var Highest = ListTimesheet.FirstOrDefault();
                        return Highest;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    var ListTimesheet = await (from em in _context.Employee
                                               join d in _context.Departments on em.Department_ID equals d.Department_ID
                                               where
                                               d.Department_ID == Department_ID
                                               select new HighestWorkingHourOfEmployee_Model
                                               {
                                                   Employee_ID = em.Employee_ID,
                                                   Name = em.Name,
                                                   Department_ID = _context.Departments.FirstOrDefault(p => p.Department_ID == em.Department_ID).Department_ID,
                                                   Department_Name = _context.Departments.FirstOrDefault(p => p.Department_ID == em.Department_ID).Department_Name,
                                                   WorkingHour = _context.Timesheet.Where(p => p.Employee_ID == em.Employee_ID & p.CheckIn != null & p.CheckIn.Value.Month == Month & p.CheckIn.Value.Year == Year & p.CheckOut != null).Sum(o => o.CheckOut.Value.Hour - o.CheckIn.Value.Hour)
                                               }).OrderByDescending(o => o.WorkingHour).ToListAsync();
                    if (ListTimesheet.Count() > 0)
                    {
                        var Highest = ListTimesheet.FirstOrDefault();
                        return Highest;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
           else
            {
                return null;
            }
        }
        public async Task<List<CaculateSalary_Model>> CalculateSalary(int? HR_ID, int? Department_ID, int? Year, int? Month)
        {
            var CheckManager = (from em in _context.Employee
                                join d in _context.Departments on em.Department_ID equals d.Department_ID
                                where
                                em.Employee_ID == HR_ID
                                & d.Department_Name.ToUpper().Contains("HR")
                                select new
                                {
                                    em.Employee_ID
                                }).SingleOrDefault();
            if (CheckManager != null)
            {
                if (Department_ID == 0)
                {
                    var ListTimeSheet = await (from em in _context.Employee
                                               select new HighestWorkingHourOfEmployee_Model
                                               {
                                                   Employee_ID = em.Employee_ID,
                                                   Name = em.Name,
                                                   Department_ID = _context.Departments.FirstOrDefault(p => p.Department_ID == em.Department_ID).Department_ID,
                                                   Department_Name = _context.Departments.FirstOrDefault(p => p.Department_ID == em.Department_ID).Department_Name,
                                                   WorkingHour = _context.Timesheet.Where(p => p.Employee_ID == em.Employee_ID & p.CheckIn != null & p.CheckIn.Value.Month == Month & p.CheckIn.Value.Year == Year & p.CheckOut != null).Sum(o => o.CheckOut.Value.Hour - o.CheckIn.Value.Hour),
                                               }).ToListAsync();
                    if (ListTimeSheet.Count() > 0)
                    { 
                        var newList = (from d in ListTimeSheet
                                       select new CaculateSalary_Model
                                       {
                                           Employee_ID = d.Employee_ID,
                                           Name = d.Name,
                                           Department_ID = d.Department_ID,
                                           Department_Name = d.Department_Name,
                                           WorkingHour = d.WorkingHour,
                                           Salary = d.WorkingHour * (_context.RegularPayment.SingleOrDefault(p=>p.Department_ID == d.Department_ID) != null ? _context.RegularPayment.SingleOrDefault(p => p.Department_ID == d.Department_ID).RegularPaymentPerHour : 0)
                                       }).ToList();
                        return newList;
                    }
                    else
                    {
                        return null;
                    }
                    
                }
                else
                {
                    var ListTimeSheet = await (from em in _context.Employee
                                               join d in _context.Departments on em.Department_ID equals d.Department_ID
                                               where
                                               d.Department_ID == Department_ID
                                               select new HighestWorkingHourOfEmployee_Model
                                               {
                                                   Employee_ID = em.Employee_ID,
                                                   Name = em.Name,
                                                   Department_ID = _context.Departments.FirstOrDefault(p => p.Department_ID == em.Department_ID).Department_ID,
                                                   Department_Name = _context.Departments.FirstOrDefault(p => p.Department_ID == em.Department_ID).Department_Name,
                                                   WorkingHour = _context.Timesheet.Where(p => p.Employee_ID == em.Employee_ID & p.CheckIn != null & p.CheckIn.Value.Month == Month & p.CheckIn.Value.Year == Year & p.CheckOut != null).Sum(o => o.CheckOut.Value.Hour - o.CheckIn.Value.Hour)
                                               }).ToListAsync();
                    if (ListTimeSheet.Count() > 0)
                    {
                        var newList = (from d in ListTimeSheet
                                       select new CaculateSalary_Model
                                       {
                                           Employee_ID = d.Employee_ID,
                                           Name = d.Name,
                                           Department_ID = d.Department_ID,
                                           Department_Name = d.Department_Name,
                                           WorkingHour = d.WorkingHour,
                                           Salary = d.WorkingHour * (_context.RegularPayment.SingleOrDefault(p => p.Department_ID == d.Department_ID) != null ? _context.RegularPayment.SingleOrDefault(p => p.Department_ID == d.Department_ID).RegularPaymentPerHour : 0)
                                       }).ToList();
                        return newList;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            else
            {
                return null;
            }
        }
    }
}
