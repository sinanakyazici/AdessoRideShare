using AdessoRideShare.Domain.Validations.Booking;
using System;

namespace AdessoRideShare.Domain.Commands.Booking
{
    public class UpdateBookingCommand : BookingCommand
    {
        public UpdateBookingCommand(Guid id, Guid customerId, Guid ridePlanId, int bookedSeatCount)
        {
            Id = id;
            CustomerId = customerId;
            RidePlanId = ridePlanId;
            BookedSeatCount = bookedSeatCount;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateBookingCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
