using System;
using AdessoRideShare.Domain.Core.Commands;

namespace AdessoRideShare.Domain.Commands.Customer
{
    public abstract class CustomerCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Email { get; protected set; }
    }
}