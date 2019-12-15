using AdessoRideShare.Domain.Validations.Booking;
using System;

namespace AdessoRideShare.Domain.Commands.Booking
{
    public class AddBookingCommand : BookingCommand
    {
        public AddBookingCommand(Guid customerId, Guid ridePlanId, int bookedSeatCount)
        {
            CustomerId = customerId;
            RidePlanId = ridePlanId;
            BookedSeatCount = bookedSeatCount;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddBookingCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
