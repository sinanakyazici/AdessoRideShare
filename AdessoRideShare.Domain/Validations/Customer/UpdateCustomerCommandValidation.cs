using AdessoRideShare.Domain.Commands.Customer;

namespace AdessoRideShare.Domain.Validations.Customer
{
    public class UpdateCustomerCommandValidation : CustomerValidation<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateEmail();
        }
    }
}