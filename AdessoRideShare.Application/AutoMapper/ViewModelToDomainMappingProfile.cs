using AutoMapper;
using AdessoRideShare.Application.ViewModels;
using AdessoRideShare.Domain.Commands.Customer;
using AdessoRideShare.Domain.Commands.RidePlan;
using AdessoRideShare.Domain.Commands.Booking;

namespace AdessoRideShare.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerViewModel, AddCustomerCommand>()
                .ConstructUsing(c => new AddCustomerCommand(c.Name, c.Email));
            CreateMap<CustomerViewModel, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.Email));

            CreateMap<RidePlanViewModel, AddRidePlanCommand>()
                .ConstructUsing(c => new AddRidePlanCommand(c.CustomerId, c.FromCityId, c.ToCityId, c.Date, c.Description, c.SeatCount, c.IsPublished));
            CreateMap<RidePlanViewModel, UpdateRidePlanCommand>()
                .ConstructUsing(c => new UpdateRidePlanCommand(c.Id, c.CustomerId, c.FromCityId, c.ToCityId, c.Date, c.Description, c.SeatCount, c.IsPublished));

            CreateMap<BookingViewModel, AddBookingCommand>()
                .ConstructUsing(c => new AddBookingCommand(c.CustomerId, c.RidePlanId, c.BookedSeatCount));
            CreateMap<BookingViewModel, UpdateBookingCommand>()
                .ConstructUsing(c => new UpdateBookingCommand(c.Id, c.CustomerId, c.RidePlanId, c.BookedSeatCount));
        }
    }
}
