using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
namespace MyApiNetCore6.Repositories
{
    public interface IRegularPaymentRepository
    {
        public Task<List<RegularPaymentList_Model>> GetAllRegularPaymentsAsync();
        public Task<RegularPaymentList_Model> GetRegularPaymentAsync(int RegularPayment_ID);
        public Task<int> AddRegularPaymentAsync(RegularPayment_Model model);
        public Task<int> UpdateRegularPaymentAsync(RegularPayment_Model model);
        public Task<int> DeleteRegularPaymentAsync(int RegularPayment_ID);
    }
}
