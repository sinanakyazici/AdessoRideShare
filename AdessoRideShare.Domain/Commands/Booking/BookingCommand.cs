using AdessoRideShare.Domain.Core.Commands;
using System;

namespace AdessoRideShare.Domain.Commands.Booking
{
    public abstract class BookingCommand : Command
    {
        public Guid Id { get; protected set; }
        public Guid CustomerId { get; protected set; }
        public Guid RidePlanId { get; protected set; }
        public int BookedSeatCount { get; protected set; }
    }
}
