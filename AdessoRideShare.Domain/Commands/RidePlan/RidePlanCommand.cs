using System;
using AdessoRideShare.Domain.Core.Commands;

namespace AdessoRideShare.Domain.Commands.RidePlan
{
    public abstract class RidePlanCommand : Command
    {
        public Guid Id { get; protected set; }
        public Guid CustomerId { get; protected set; }
        public int FromCityId { get; protected set; }
        public int ToCityId { get; protected set; }
        public DateTime Date { get; protected set; }
        public string Description { get; protected set; }
        public int SeatCount { get; protected set; }
        public bool IsPublished { get; protected set; }
    }
}