using System;
using AdessoRideShare.Domain.Core.Events;

namespace AdessoRideShare.Domain.Events.Customer
{
    public class CustomerRemovedEvent : Event
    {
        public CustomerRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}