using System;
using System.Threading;
using System.Threading.Tasks;
using AdessoRideShare.Domain.Commands.Customer;
using AdessoRideShare.Domain.Core.Bus;
using AdessoRideShare.Domain.Core.Notifications;
using AdessoRideShare.Domain.Events.Customer;
using AdessoRideShare.Domain.Interfaces;
using AdessoRideShare.Domain.Models;
using MediatR;

namespace AdessoRideShare.Domain.CommandHandlers
{
    public class CustomerCommandHandler : CommandHandler,
        IRequestHandler<AddCustomerCommand, bool>,
        IRequestHandler<UpdateCustomerCommand, bool>,
        IRequestHandler<RemoveCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler Bus;

        public CustomerCommandHandler(ICustomerRepository customerRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _customerRepository = customerRepository;
            Bus = bus;
        }

        public Task<bool> Handle(AddCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var customer = new Customer(Guid.NewGuid(), message.Name, message.Email);

            if (_customerRepository.GetByEmail(customer.Email) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The customer e-mail has already been taken."));
                return Task.FromResult(false);
            }

            _customerRepository.Add(customer);

            if (Commit())
            {
                Bus.RaiseEvent(new CustomerAddedEvent(customer.Id, customer.Name, customer.Email));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var customer = new Customer(message.Id, message.Name, message.Email);
            var existingCustomer = _customerRepository.GetByEmail(customer.Email);

            if (existingCustomer != null && existingCustomer.Id != customer.Id)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The customer e-mail has already been taken."));
                return Task.FromResult(false);
            }

            _customerRepository.Update(customer);

            if (Commit())
            {
                Bus.RaiseEvent(new CustomerUpdatedEvent(customer.Id, customer.Name, customer.Email));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            _customerRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new CustomerRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _customerRepository.Dispose();
        }
    }
}