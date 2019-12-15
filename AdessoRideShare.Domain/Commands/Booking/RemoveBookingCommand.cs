using AdessoRideShare.Domain.Validations.Booking;
using System;

namespace AdessoRideShare.Domain.Commands.Booking
{
    public class RemoveBookingCommand : BookingCommand
    {
        public RemoveBookingCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveBookingCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
