using AdessoRideShare.Domain.Core.Events;
using System;

namespace AdessoRideShare.Domain.Events.Booking
{
    public class BookingAddedEvent : Event
    {
        public BookingAddedEvent(Guid id, Guid customerId, Guid ridePlanId, int bookedSeatCount)
        {
            Id = id;
            CustomerId = customerId;
            RidePlanId = ridePlanId;
            BookedSeatCount = bookedSeatCount;
            AggregateId = id;
        }

        public Guid Id { get; set; }
        public Guid CustomerId { get; private set; }
        public Guid RidePlanId { get; private set; }
        public int BookedSeatCount { get; private set; }
    }
}
