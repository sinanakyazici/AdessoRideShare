using AdessoRideShare.Domain.Commands.Booking;

namespace AdessoRideShare.Domain.Validations.Booking
{
    public class UpdateBookingCommandValidation : BookingValidation<UpdateBookingCommand>
    {
        public UpdateBookingCommandValidation()
        {
            ValidateId();
            ValidateCustomerId();
            ValidateRidePlanId();
            ValidateBookedSeatCount();
        }
    }
}
