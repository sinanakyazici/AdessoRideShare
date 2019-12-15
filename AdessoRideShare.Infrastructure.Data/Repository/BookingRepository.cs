using AdessoRideShare.Domain.Interfaces;
using AdessoRideShare.Domain.Models;
using AdessoRideShare.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdessoRideShare.Infrastructure.Data.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(AdessoRideShareContext context)
                : base(context)
        {

        }

        public Booking Get(Guid customerId, Guid ridePlanId)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.CustomerId == customerId && c.RidePlanId == ridePlanId);
        }

        public IEnumerable<Booking> GetCustomerBookings(Guid customerId)
        {
            return DbSet.AsNoTracking().Where(c => c.CustomerId == customerId);
        }

        public int GetTotalBookedSeatCountByRidePlanId(Guid ridePlanId)
        {
            return DbSet.AsNoTracking().Where(c => c.RidePlanId == ridePlanId).Sum(x => x.BookedSeatCount);
        }
    }
}
