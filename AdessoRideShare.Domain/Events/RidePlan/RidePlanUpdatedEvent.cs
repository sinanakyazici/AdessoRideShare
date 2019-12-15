using AdessoRideShare.Domain.Core.Events;
using System;

namespace AdessoRideShare.Domain.Events.RidePlan
{
    public class RidePlanUpdatedEvent : Event
    {
        public RidePlanUpdatedEvent(Guid id, Guid customerId, int fromCityId, int toCityId, DateTime date, string description, int seatCount, bool isPublished)
        {
            Id = id;
            CustomerId = customerId;
            FromCityId = fromCityId;
            ToCityId = toCityId;
            Date = date;
            Description = description;
            SeatCount = seatCount;
            IsPublished = isPublished;
            AggregateId = id;
        }
        public Guid Id { get; set; }

        public Guid CustomerId { get; private set; }
        public int FromCityId { get; private set; }
        public int ToCityId { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public int SeatCount { get; private set; }
        public bool IsPublished { get; private set; }
    }
}
