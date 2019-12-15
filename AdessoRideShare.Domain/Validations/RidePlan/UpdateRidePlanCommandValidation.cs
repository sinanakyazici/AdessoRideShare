using AdessoRideShare.Domain.Commands.RidePlan;

namespace AdessoRideShare.Domain.Validations.RidePlan
{
    public class UpdateRidePlanCommandValidation : RidePlanValidation<UpdateRidePlanCommand>
    {
        public UpdateRidePlanCommandValidation()
        {
            ValidateId();
            ValidateCustomerId();
            ValidateFromCity();
            ValidateToCity();
            ValidateDescription();
            ValidateSeatCount();
        }
    }
}