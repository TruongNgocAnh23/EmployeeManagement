using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
namespace MyApiNetCore6.Repositories
{
    public interface IManagerFunctionsRepository
    {
        public Task<List<GetListTimesheet_Model>> GetListTimeSheetAsync(int? Manager_ID, int? Year, int? Month);
        public Task<int> AddTimesheetAsync(AddTimesheet_Model model);
        public Task<int> UpdateTimesheetAsync(UpdateTimesheet_Model model);
        public Task<int> DeleteTimesheetAsync(DeleteTimesheet_Model model);
    }
}
