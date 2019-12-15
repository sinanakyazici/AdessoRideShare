using AdessoRideShare.Domain.Commands.RidePlan;

namespace AdessoRideShare.Domain.Validations.RidePlan
{
    public class RemoveRidePlanCommandValidation : RidePlanValidation<RemoveRidePlanCommand>
    {
        public RemoveRidePlanCommandValidation()
        {
            ValidateId();
        }
    }
}