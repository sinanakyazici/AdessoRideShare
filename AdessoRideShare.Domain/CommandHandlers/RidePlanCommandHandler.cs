using AdessoRideShare.Domain.Commands.RidePlan;
using AdessoRideShare.Domain.Core.Bus;
using AdessoRideShare.Domain.Core.Notifications;
using AdessoRideShare.Domain.Events.RidePlan;
using AdessoRideShare.Domain.Interfaces;
using AdessoRideShare.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AdessoRideShare.Domain.CommandHandlers
{
    public class RidePlanCommandHandler : CommandHandler,
                IRequestHandler<AddRidePlanCommand, bool>,
                IRequestHandler<UpdateRidePlanCommand, bool>,
                IRequestHandler<RemoveRidePlanCommand, bool>
    {
        private readonly IRidePlanRepository _ridePlanRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler Bus;

        public RidePlanCommandHandler(IRidePlanRepository ridePlanRepository,
                                     ICustomerRepository customerRepository,
                                     IUnitOfWork uow,
                                     IMediatorHandler bus,
                                     INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _ridePlanRepository = ridePlanRepository;
            _customerRepository = customerRepository;
            Bus = bus;
        }


        public Task<bool> Handle(AddRidePlanCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            if (_customerRepository.GetById(message.CustomerId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The customer has not been found."));
                return Task.FromResult(false);
            }

            var ridePlan = new RidePlan(Guid.NewGuid(), message.CustomerId, message.FromCityId, message.ToCityId, message.Date, message.Description, message.SeatCount, message.IsPublished);

            if (_ridePlanRepository.Get(ridePlan.CustomerId, ridePlan.FromCityId, ridePlan.ToCityId, ridePlan.Date) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The ride plan has already been created before."));
                return Task.FromResult(false);
            }

            _ridePlanRepository.Add(ridePlan);

            if (Commit())
            {
                Bus.RaiseEvent(new RidePlanAddedEvent(ridePlan.Id, ridePlan.CustomerId, ridePlan.FromCityId, ridePlan.ToCityId, ridePlan.Date, ridePlan.Description, ridePlan.SeatCount, ridePlan.IsPublished));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateRidePlanCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            if (_customerRepository.GetById(message.CustomerId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The customer has not been found."));
                return Task.FromResult(false);
            }

            var ridePlan = new RidePlan(message.Id, message.CustomerId, message.FromCityId, message.ToCityId, message.Date, message.Description, message.SeatCount, message.IsPublished);
            var existingRidePlan = _ridePlanRepository.Get(message.CustomerId, message.FromCityId, message.ToCityId, message.Date);

            if (existingRidePlan != null &&
                existingRidePlan.Id != ridePlan.Id &&
                existingRidePlan.CustomerId == ridePlan.CustomerId &&
                existingRidePlan.FromCityId == ridePlan.FromCityId &&
                existingRidePlan.ToCityId == ridePlan.ToCityId &&
                existingRidePlan.Date == ridePlan.Date)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The ride plan has already been created before."));
                return Task.FromResult(false);
            }

            _ridePlanRepository.Update(ridePlan);

            if (Commit())
            {
                Bus.RaiseEvent(new RidePlanUpdatedEvent(ridePlan.Id, ridePlan.CustomerId, ridePlan.FromCityId, ridePlan.ToCityId, ridePlan.Date, ridePlan.Description, ridePlan.SeatCount, ridePlan.IsPublished));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveRidePlanCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            _customerRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new RidePlanRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _customerRepository.Dispose();
        }
    }
}
