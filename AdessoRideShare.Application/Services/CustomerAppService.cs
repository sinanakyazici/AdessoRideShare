using AdessoRideShare.Application.EventSourcedNormalizers.Customer;
using AdessoRideShare.Application.Interfaces;
using AdessoRideShare.Application.ViewModels;
using AdessoRideShare.Domain.Commands.Customer;
using AdessoRideShare.Domain.Core.Bus;
using AdessoRideShare.Domain.Interfaces;
using AdessoRideShare.Infrastructure.Data.Repository.EventSourcing;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;

namespace AdessoRideShare.Application.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public CustomerAppService(IMapper mapper,
                                  ICustomerRepository customerRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            return _customerRepository.GetAll().ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider);
        }

        public CustomerViewModel GetById(Guid id)
        {
            return _mapper.Map<CustomerViewModel>(_customerRepository.GetById(id));
        }

        public void Add(CustomerViewModel customerViewModel)
        {
            var addCommand = _mapper.Map<AddCustomerCommand>(customerViewModel);
            Bus.SendCommand(addCommand);
        }

        public void Update(CustomerViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCustomerCommand>(customerViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveCustomerCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<CustomerHistoryData> GetAllHistory(Guid id)
        {
            return CustomerHistory.ToJavaScriptCustomerHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        } 
    }
}
