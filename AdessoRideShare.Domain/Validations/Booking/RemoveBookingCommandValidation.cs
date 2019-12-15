using AdessoRideShare.Domain.Commands.Booking;

namespace AdessoRideShare.Domain.Validations.Booking
{
    public class RemoveBookingCommandValidation : BookingValidation<RemoveBookingCommand>
    {
        public RemoveBookingCommandValidation()
        {
            ValidateId();
        }
    }
}
