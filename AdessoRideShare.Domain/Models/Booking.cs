using AdessoRideShare.Domain.Core.Models;
using System;

namespace AdessoRideShare.Domain.Models
{
    public class Booking : Entity
    {
        public Booking(Guid id, Guid customerId, Guid ridePlanId, int bookedSeatCount)
        {
            Id = id;
            CustomerId = customerId;
            RidePlanId = ridePlanId;
            BookedSeatCount = bookedSeatCount;
        }

        protected Booking()
        {

        }

        public Guid CustomerId { get; private set; }
        public Guid RidePlanId { get; private set; }
        public int BookedSeatCount { get; private set; }
    }
}
