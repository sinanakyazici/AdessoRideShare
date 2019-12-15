using AdessoRideShare.Domain.Validations.Customer;

namespace AdessoRideShare.Domain.Commands.Customer
{
    public class AddCustomerCommand : CustomerCommand
    {
        public AddCustomerCommand(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}