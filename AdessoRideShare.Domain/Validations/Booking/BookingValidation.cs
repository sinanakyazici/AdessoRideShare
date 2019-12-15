using AdessoRideShare.Domain.Commands.Booking;
using FluentValidation;
using System;

namespace AdessoRideShare.Domain.Validations.Booking
{
    public abstract class BookingValidation<T> : AbstractValidator<T> where T : BookingCommand
    {
        protected void ValidateCustomerId()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty).WithMessage("Please ensure you have entered a customer");
        }

        protected void ValidateRidePlanId()
        {
            RuleFor(c => c.RidePlanId)
                .NotEqual(Guid.Empty).WithMessage("Please ensure you have entered a ride plan");
        }

        protected void ValidateBookedSeatCount()
        {
            RuleFor(c => c.BookedSeatCount)
                .LessThanOrEqualTo(0).WithMessage("Please ensure you have entered a booked seat count");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
