using AutoMapper;
using AdessoRideShare.Application.ViewModels;
using AdessoRideShare.Domain.Models;

namespace AdessoRideShare.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<RidePlan, RidePlanViewModel>();
            CreateMap<Booking, BookingViewModel>();
        }
    }
}
