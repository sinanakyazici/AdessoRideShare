using AdessoRideShare.Domain.Commands.Customer;

namespace AdessoRideShare.Domain.Validations.Customer
{
    public class RemoveCustomerCommandValidation : CustomerValidation<RemoveCustomerCommand>
    {
        public RemoveCustomerCommandValidation()
        {
            ValidateId();
        }
    }
}