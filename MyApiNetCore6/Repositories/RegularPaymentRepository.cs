using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using System.Text.Json.Serialization;
using System.Linq;

namespace MyApiNetCore6.Repositories
{
    public class RegularPaymentRepository : IRegularPaymentRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;
        private const int Result = 0;
        public RegularPaymentRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RegularPaymentList_Model>> GetAllRegularPaymentsAsync()
        {
            var regularP = await (from rp in _context.RegularPayment
                                  join d in _context.Departments! on rp.Department_ID equals d.Department_ID
                                  select new RegularPaymentList_Model
                                  {
                                      RegularPayment_ID = rp.RegularPayment_ID,
                                      Department_Name = d.Department_Name,
                                      RegularPaymentPerHour = rp.RegularPaymentPerHour
                                  }).ToListAsync();
            return regularP;
        }

        public async Task<RegularPaymentList_Model> GetRegularPaymentAsync(int RegularPayment_ID)
        {
            var regularP = await (from rp in _context.RegularPayment
                                  join d in _context.Departments! on rp.Department_ID equals d.Department_ID
                                  where rp.RegularPayment_ID == RegularPayment_ID
                                  select new RegularPaymentList_Model
                                  {
                                      RegularPayment_ID = rp.RegularPayment_ID,
                                      Department_Name = d.Department_Name,
                                      RegularPaymentPerHour = rp.RegularPaymentPerHour
                                  }).ToListAsync();
            return regularP.FirstOrDefault()!;

        }
      
        public async Task<int> AddRegularPaymentAsync(RegularPayment_Model model)
        {
            if (model.RegularPaymentPerHour <= 0)
            {
                return 1;
            }

            var CheckDepartment = _context.Departments.SingleOrDefault(p => p.Department_ID == model.Department_ID);
            if (CheckDepartment != null)
            {
                var CheckID = _context.RegularPayment.SingleOrDefault(p => p.RegularPayment_ID == model.RegularPayment_ID);
                if (CheckID == null)
                {
                    var newRegularPament = _mapper.Map<RegularPayment>(model);
                    _context.RegularPayment!.Add(newRegularPament);
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

        public async Task<int> UpdateRegularPaymentAsync(RegularPayment_Model model)
        {
            if (model.RegularPaymentPerHour <= 0)
            {
                return 1;
            }
            var Update = _context.RegularPayment!.FirstOrDefault(p => p.RegularPayment_ID == model.RegularPayment_ID);
            if (Update != null)
            {
                Update.RegularPaymentPerHour = model.RegularPaymentPerHour;
                Update.Department_ID = model.Department_ID;
                await _context.SaveChangesAsync();
                return Update.RegularPayment_ID;
            }
            else
            {
                return Result;
            }    
        }

        public async Task<int> DeleteRegularPaymentAsync(int RegularPayment_ID)
        {
            var Dele = _context.RegularPayment!.FirstOrDefault(p => p.RegularPayment_ID == RegularPayment_ID);
            if (Dele != null)
            {
                _context.RegularPayment.Remove(Dele);
                await _context.SaveChangesAsync();

                return Dele.RegularPayment_ID;
            }
            else
            {
                return Result;
            }
        }
    }
}
