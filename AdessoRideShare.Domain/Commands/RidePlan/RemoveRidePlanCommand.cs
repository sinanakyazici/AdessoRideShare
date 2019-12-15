using System;
using AdessoRideShare.Domain.Validations.RidePlan;

namespace AdessoRideShare.Domain.Commands.RidePlan
{
    public class RemoveRidePlanCommand : RidePlanCommand
    {
        public RemoveRidePlanCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveRidePlanCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}