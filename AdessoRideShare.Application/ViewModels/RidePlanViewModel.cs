using System;

namespace AdessoRideShare.Application.ViewModels
{
    public class RidePlanViewModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; private set; }
        public string CustomerName { get; set; }
        public int FromCityId { get; private set; }
        public int ToCityId { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public int SeatCount { get; private set; }
        public bool IsPublished { get; private set; }
    }
}
