using AdessoRideShare.Domain.Commands.RidePlan;
using FluentValidation;
using System;

namespace AdessoRideShare.Domain.Validations.RidePlan
{
    public abstract class RidePlanValidation<T> : AbstractValidator<T> where T : RidePlanCommand
    {
        protected void ValidateCustomerId()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty).WithMessage("Please ensure you have entered a customer");
        }

        protected void ValidateFromCity()
        {
            RuleFor(c => c.FromCityId)
                .LessThanOrEqualTo(0).WithMessage("Please ensure you have entered a valid city");
        }

        protected void ValidateToCity()
        {
            RuleFor(c => c.ToCityId)
                .LessThanOrEqualTo(0).WithMessage("Please ensure you have entered a valid city");
        }
        protected void ValidateDescription()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Please ensure you have entered the description")
                .Length(2, 150).WithMessage("The Description must have between 2 and 150 characters");
        }
        protected void ValidateSeatCount()
        {
            RuleFor(c => c.SeatCount)
                .LessThanOrEqualTo(0).WithMessage("Please ensure you have entered a seat count");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
