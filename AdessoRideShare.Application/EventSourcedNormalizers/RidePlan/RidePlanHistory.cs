using AdessoRideShare.Domain.Core.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdessoRideShare.Application.EventSourcedNormalizers.RidePlan
{
    public class RidePlanHistory
    {
        public static IList<RidePlanHistoryData> HistoryData { get; set; }

        public static IList<RidePlanHistoryData> ToJavaScriptRidePlanHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<RidePlanHistoryData>();
            RidePlanHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<RidePlanHistoryData>();
            var last = new RidePlanHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new RidePlanHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id ? "" : change.Id,

                    CustomerId = string.IsNullOrWhiteSpace(change.CustomerId) || change.CustomerId == last.CustomerId ? "" : change.CustomerId,
                    FromCityId = string.IsNullOrWhiteSpace(change.FromCityId) || change.FromCityId == last.FromCityId ? "" : change.FromCityId,
                    ToCityId = string.IsNullOrWhiteSpace(change.ToCityId) || change.ToCityId == last.ToCityId ? "" : change.ToCityId,
                    Date = string.IsNullOrWhiteSpace(change.Date) || change.Date == last.Date ? "" : change.Date,
                    Description = string.IsNullOrWhiteSpace(change.Description) || change.Description == last.Description ? "" : change.Description,
                    SeatCount = string.IsNullOrWhiteSpace(change.SeatCount) || change.SeatCount == last.SeatCount ? "" : change.SeatCount,
                    IsPublished = string.IsNullOrWhiteSpace(change.IsPublished) || change.IsPublished == last.IsPublished ? "" : change.IsPublished,

                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void RidePlanHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new RidePlanHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "RidePlanAddedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.CustomerId = values["CustomerId"];
                        slot.FromCityId = values["FromCityId"];
                        slot.ToCityId = values["ToCityId"];
                        slot.Date = values["Date"];
                        slot.Description = values["Description"];
                        slot.SeatCount = values["SeatCount"];
                        slot.IsPublished = values["IsPublished"];
                        slot.Action = "Added";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "RidePlanUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.CustomerId = values["CustomerId"];
                        slot.FromCityId = values["FromCityId"];
                        slot.ToCityId = values["ToCityId"];
                        slot.Date = values["Date"];
                        slot.Description = values["Description"];
                        slot.SeatCount = values["SeatCount"];
                        slot.IsPublished = values["IsPublished"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "RidePlanRemovedEvent":
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
