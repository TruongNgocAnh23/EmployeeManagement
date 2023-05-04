using AutoMapper;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;

namespace MyApiNetCore6.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Book, BookModel>().ReverseMap();
            CreateMap<Department, Department_Model>().ReverseMap();
            CreateMap<RegularPayment, RegularPayment_Model>().ReverseMap();
            CreateMap<RegularPaymentList, RegularPaymentList_Model>().ReverseMap();
            CreateMap<Employee, Employee_Model>().ReverseMap();
        }
    }
}
