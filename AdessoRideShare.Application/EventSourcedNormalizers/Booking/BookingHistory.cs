using AdessoRideShare.Domain.Core.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdessoRideShare.Application.EventSourcedNormalizers.Booking
{
    public class BookingHistory
    {
        public static IList<BookingHistoryData> HistoryData { get; set; }

        public static IList<BookingHistoryData> ToJavaScriptBookingHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<BookingHistoryData>();
            BookingHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<BookingHistoryData>();
            var last = new BookingHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new BookingHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id ? "" : change.Id,

                    CustomerId = string.IsNullOrWhiteSpace(change.CustomerId) || change.CustomerId == last.CustomerId ? "" : change.CustomerId,
                    RidePlanId = string.IsNullOrWhiteSpace(change.RidePlanId) || change.RidePlanId == last.RidePlanId ? "" : change.RidePlanId,
                    BookedSeatCount = string.IsNullOrWhiteSpace(change.BookedSeatCount) || change.BookedSeatCount == last.BookedSeatCount ? "" : change.BookedSeatCount,

                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void BookingHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new BookingHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "BookingAddedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.CustomerId = values["CustomerId"];
                        slot.RidePlanId = values["RidePlanId"];
                        slot.BookedSeatCount = values["BookedSeatCount"];
                        slot.Action = "Added";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "BookingUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.CustomerId = values["CustomerId"];
                        slot.RidePlanId = values["RidePlanId"];
                        slot.BookedSeatCount = values["BookedSeatCount"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "BookingRemovedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Action = "Removed";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                }
                HistoryData.Add(slot);
            }
        }
    }
}
