using AdessoRideShare.Domain.Commands.Customer;

namespace AdessoRideShare.Domain.Validations.Customer
{
    public class AddCustomerCommandValidation : CustomerValidation<AddCustomerCommand>
    {
        public AddCustomerCommandValidation()
        {
            ValidateName();
            ValidateEmail();
        }
    }
}