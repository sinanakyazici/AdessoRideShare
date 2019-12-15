
using AdessoRideShare.Domain.Commands.Booking;

namespace AdessoRideShare.Domain.Validations.Booking
{
    public class AddBookingCommandValidation : BookingValidation<AddBookingCommand>
    {
        public AddBookingCommandValidation()
        {
            ValidateCustomerId();
            ValidateRidePlanId();
            ValidateBookedSeatCount();
        }
    }
}
