using System;
using AdessoRideShare.Domain.Validations.RidePlan;

namespace AdessoRideShare.Domain.Commands.RidePlan
{
    public class UpdateRidePlanCommand : RidePlanCommand
    {
        public UpdateRidePlanCommand(Guid id, Guid customerId, int fromCityId, int toCityId, DateTime date, string description, int seatCount, bool isPlublised)
        {
            Id = id;
            CustomerId = customerId;
            FromCityId = fromCityId;
            ToCityId = toCityId;
            Date = date;
            Description = description;
            SeatCount = seatCount;
            IsPublished = isPlublised;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateRidePlanCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}