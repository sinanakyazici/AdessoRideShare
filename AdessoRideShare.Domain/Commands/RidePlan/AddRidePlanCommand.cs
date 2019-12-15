using System;
using AdessoRideShare.Domain.Validations.RidePlan;

namespace AdessoRideShare.Domain.Commands.RidePlan
{
    public class AddRidePlanCommand : RidePlanCommand
    {
        public AddRidePlanCommand(Guid customerId, int fromCityId, int toCityId, DateTime date, string description, int seatCount, bool isPublished)
        {
            CustomerId = customerId;
            FromCityId = fromCityId;
            ToCityId = toCityId;
            Date = date;
            Description = description;
            SeatCount = seatCount;
            IsPublished = isPublished;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddRidePlanCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}