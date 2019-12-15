using System;

namespace AdessoRideShare.Application.ViewModels
{
    public class BookingViewModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; private set; }
        public Guid RidePlanId { get; private set; }
        public string CustomerName { get; set; }
        public int FromCityId { get; private set; }
        public int ToCityId { get; private set; }
        public DateTime Date { get; private set; }
        public int BookedSeatCount { get; private set; }
    }
}
