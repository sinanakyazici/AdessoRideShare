namespace AdessoRideShare.Application.EventSourcedNormalizers.Booking
{
    public class BookingHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string RidePlanId { get; set; }
        public string BookedSeatCount { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}
