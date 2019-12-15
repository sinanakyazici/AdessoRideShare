using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Application.EventSourcedNormalizers.RidePlan
{
    public class RidePlanHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string FromCityId { get; set; }
        public string ToCityId { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string SeatCount { get; set; }
        public string IsPublished { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}
