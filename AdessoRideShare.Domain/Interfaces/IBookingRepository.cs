using AdessoRideShare.Domain.Models;
using System;
using System.Collections.Generic;

namespace AdessoRideShare.Domain.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        IEnumerable<Booking> GetCustomerBookings(Guid customerId);
        Booking Get(Guid customerId, Guid ridePlanId);
        int GetTotalBookedSeatCountByRidePlanId(Guid ridePlanId);
    }
}
