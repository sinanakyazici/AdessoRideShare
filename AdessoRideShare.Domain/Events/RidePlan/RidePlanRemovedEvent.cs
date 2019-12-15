using AdessoRideShare.Domain.Core.Events;
using System;

namespace AdessoRideShare.Domain.Events.RidePlan
{
    public class RidePlanRemovedEvent : Event
    {
        public RidePlanRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}