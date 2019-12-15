﻿using System;
using AdessoRideShare.Domain.Validations.Customer;

namespace AdessoRideShare.Domain.Commands.Customer
{
    public class UpdateCustomerCommand : CustomerCommand
    {
        public UpdateCustomerCommand(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}