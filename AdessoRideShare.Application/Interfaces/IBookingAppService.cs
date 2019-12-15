using AdessoRideShare.Application.EventSourcedNormalizers.Booking;
using AdessoRideShare.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace AdessoRideShare.Application.Interfaces
{
    public interface IBookingAppService : IDisposable
    {
        void Add(BookingViewModel bookingViewModel);
        IEnumerable<BookingViewModel> GetAll();
        BookingViewModel GetById(Guid id);
        void Update(BookingViewModel bookingViewModel);
        void Remove(Guid id);

        IList<BookingHistoryData> GetAllHistory(Guid id);
        IEnumerable<BookingViewModel> GetCustomerBookings(Guid customerId);
    }
}
