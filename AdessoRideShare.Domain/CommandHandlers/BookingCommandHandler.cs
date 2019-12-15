using AdessoRideShare.Domain.Commands.Booking;
using AdessoRideShare.Domain.Core.Bus;
using AdessoRideShare.Domain.Core.Notifications;
using AdessoRideShare.Domain.Events.Booking;
using AdessoRideShare.Domain.Interfaces;
using AdessoRideShare.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AdessoRideShare.Domain.CommandHandlers
{
    public class BookingCommandHandler : CommandHandler,
        IRequestHandler<AddBookingCommand, bool>,
        IRequestHandler<UpdateBookingCommand, bool>,
        IRequestHandler<RemoveBookingCommand, bool>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRidePlanRepository _ridePlanRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler Bus;

        public BookingCommandHandler(IBookingRepository bookingRepository,
                                      ICustomerRepository customerRepository,
                                      IRidePlanRepository ridePlanRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _bookingRepository = bookingRepository;
            _ridePlanRepository = ridePlanRepository;
            _customerRepository = customerRepository;
            Bus = bus;
        }

        public Task<bool> Handle(AddBookingCommand message, CancellationToken cancellationToken)
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

            var ridePlan = _ridePlanRepository.GetById(message.RidePlanId);
            if (ridePlan != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The ride plan has not been found."));
                return Task.FromResult(false);
            }

            if (!ridePlan.IsPublished)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The ride plan is not published."));
                return Task.FromResult(false);
            }

            var booking = new Booking(Guid.NewGuid(), message.CustomerId, message.RidePlanId, message.BookedSeatCount);

            if (_bookingRepository.Get(booking.CustomerId, booking.RidePlanId) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The booking has already been created before."));
                return Task.FromResult(false);
            }

            var availableSeatCount = ridePlan.SeatCount - _bookingRepository.GetTotalBookedSeatCountByRidePlanId(booking.RidePlanId);
            if (availableSeatCount <= 0)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The ride plan is full."));
                return Task.FromResult(false);
            }

            if (availableSeatCount < booking.BookedSeatCount)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, $"There is only {availableSeatCount} seat for booking."));
                return Task.FromResult(false);
            }

            _bookingRepository.Add(booking);

            if (Commit())
            {
                Bus.RaiseEvent(new BookingAddedEvent(booking.Id, booking.CustomerId, booking.RidePlanId, booking.BookedSeatCount));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateBookingCommand message, CancellationToken cancellationToken)
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

            var ridePlan = _ridePlanRepository.GetById(message.RidePlanId);
            if (ridePlan != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The ride plan has not been found."));
                return Task.FromResult(false);
            }

            if (!ridePlan.IsPublished)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The ride plan is not published."));
                return Task.FromResult(false);
            }

            var booking = new Booking(Guid.NewGuid(), message.CustomerId, message.RidePlanId, message.BookedSeatCount);
            var existingBooking = _bookingRepository.Get(booking.CustomerId, booking.RidePlanId);

            if (existingBooking != null &&
                existingBooking.Id != booking.Id &&
                existingBooking.CustomerId == booking.CustomerId &&
                existingBooking.RidePlanId == booking.RidePlanId)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The ride plan has already been created before."));
                return Task.FromResult(false);
            }

            var availableSeatCount = ridePlan.SeatCount - _bookingRepository.GetTotalBookedSeatCountByRidePlanId(booking.RidePlanId) - existingBooking.BookedSeatCount;
            if (availableSeatCount <= 0)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The ride plan is full."));
                return Task.FromResult(false);
            }

            if (availableSeatCount < booking.BookedSeatCount)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, $"There is only {availableSeatCount} seat for booking."));
                return Task.FromResult(false);
            }

            _bookingRepository.Add(booking);

            if (Commit())
            {
                Bus.RaiseEvent(new BookingUpdatedEvent(booking.Id, booking.CustomerId, booking.RidePlanId, booking.BookedSeatCount));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveBookingCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            _bookingRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new BookingRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }
    }
}
