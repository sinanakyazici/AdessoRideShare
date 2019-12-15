using AdessoRideShare.Domain.Events.RidePlan;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AdessoRideShare.Domain.EventHandlers
{
    public class RidePlanEventHandler :
           INotificationHandler<RidePlanAddedEvent>,
           INotificationHandler<RidePlanUpdatedEvent>,
           INotificationHandler<RidePlanRemovedEvent>
    {
        public Task Handle(RidePlanUpdatedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(RidePlanAddedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(RidePlanRemovedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
