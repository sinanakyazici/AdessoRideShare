using System;
using AdessoRideShare.Domain.Core.Events;

namespace AdessoRideShare.Domain.Events.Customer
{
    public class CustomerUpdatedEvent : Event
    {
        public CustomerUpdatedEvent(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }
    }
}