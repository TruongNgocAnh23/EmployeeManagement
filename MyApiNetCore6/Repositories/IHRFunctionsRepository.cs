using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
namespace MyApiNetCore6.Repositories
{
    public interface IHRFunctionsRepository
    {
        public Task<List<HRGetListTimesheet_Model>> HRGetListTimeSheetAsync(int? HR_ID, int? Department_ID, int? Year, int? Month);
        public Task<HighestWorkingHourOfEmployee_Model> GetHighestWorkingHourOfEmployee(int? HR_ID, int? Department_ID, int? Year, int? Month);
        public Task<List<CaculateSalary_Model>> CalculateSalary(int? HR_ID, int? Department_ID, int? Year, int? Month);
    }
}
