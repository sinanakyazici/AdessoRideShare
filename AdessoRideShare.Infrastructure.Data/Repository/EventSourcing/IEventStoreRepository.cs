using System;
using System.Collections.Generic;
using AdessoRideShare.Domain.Core.Events;

namespace AdessoRideShare.Infrastructure.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}