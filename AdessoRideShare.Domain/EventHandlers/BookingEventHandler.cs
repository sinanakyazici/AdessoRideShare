using AdessoRideShare.Domain.Events.Booking;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdessoRideShare.Domain.EventHandlers
{
    public class BookingEventHandler :
           INotificationHandler<BookingAddedEvent>,
           INotificationHandler<BookingUpdatedEvent>,
           INotificationHandler<BookingRemovedEvent>
    {
        public Task Handle(BookingUpdatedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(BookingAddedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(BookingRemovedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
