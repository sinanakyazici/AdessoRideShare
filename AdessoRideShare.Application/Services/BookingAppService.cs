using AdessoRideShare.Application.EventSourcedNormalizers.Booking;
using AdessoRideShare.Application.Interfaces;
using AdessoRideShare.Application.ViewModels;
using AdessoRideShare.Domain.Commands.Booking;
using AdessoRideShare.Domain.Core.Bus;
using AdessoRideShare.Domain.Interfaces;
using AdessoRideShare.Infrastructure.Data.Repository.EventSourcing;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdessoRideShare.Application.Services
{
    public class BookingAppService : IBookingAppService
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public BookingAppService(IMapper mapper,
                                  IBookingRepository bookingRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<BookingViewModel> GetAll()
        {
            return _bookingRepository.GetAll().ProjectTo<BookingViewModel>(_mapper.ConfigurationProvider);
        }

        public BookingViewModel GetById(Guid id)
        {
            return _mapper.Map<BookingViewModel>(_bookingRepository.GetById(id));
        }

        public void Add(BookingViewModel bookingViewModel)
        {
            var addCommand = _mapper.Map<AddBookingCommand>(bookingViewModel);
            Bus.SendCommand(addCommand);
        }

        public void Update(BookingViewModel bookingViewModel)
        {
            var updateCommand = _mapper.Map<UpdateBookingCommand>(bookingViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveBookingCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<BookingHistoryData> GetAllHistory(Guid id)
        {
            return BookingHistory.ToJavaScriptBookingHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<BookingViewModel> GetCustomerBookings(Guid customerId)
        {
            return _bookingRepository.GetCustomerBookings(customerId).Select(_mapper.Map<BookingViewModel>);
        }
    }
}
