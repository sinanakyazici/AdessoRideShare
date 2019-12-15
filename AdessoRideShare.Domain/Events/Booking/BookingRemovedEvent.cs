using AdessoRideShare.Domain.Core.Events;
using System;

namespace AdessoRideShare.Domain.Events.Booking
{
    public class BookingRemovedEvent : Event
    {
        public BookingRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
