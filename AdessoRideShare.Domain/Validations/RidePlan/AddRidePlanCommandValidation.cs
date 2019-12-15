using AdessoRideShare.Domain.Commands.RidePlan;

namespace AdessoRideShare.Domain.Validations.RidePlan
{
    public class AddRidePlanCommandValidation : RidePlanValidation<AddRidePlanCommand>
    {
        public AddRidePlanCommandValidation()
        {
            ValidateCustomerId();
            ValidateFromCity();
            ValidateToCity();
            ValidateDescription();
            ValidateSeatCount();
        }
    }
}