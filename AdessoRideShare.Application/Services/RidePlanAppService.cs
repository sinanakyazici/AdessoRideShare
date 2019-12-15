using AdessoRideShare.Application.EventSourcedNormalizers.RidePlan;
using AdessoRideShare.Application.Interfaces;
using AdessoRideShare.Application.ViewModels;
using AdessoRideShare.Domain.Commands.RidePlan;
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
    public class RidePlanAppService : IRidePlanAppService
    {
        private readonly IMapper _mapper;
        private readonly IRidePlanRepository _ridePlanRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public RidePlanAppService(IMapper mapper,
                                  IRidePlanRepository ridePlanRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _ridePlanRepository = ridePlanRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<RidePlanViewModel> GetAll()
        {
            return _ridePlanRepository.GetAll().ProjectTo<RidePlanViewModel>(_mapper.ConfigurationProvider);
        }

        public RidePlanViewModel GetById(Guid id)
        {
            return _mapper.Map<RidePlanViewModel>(_ridePlanRepository.GetById(id));
        }

        public void Add(RidePlanViewModel ridePlanViewModel)
        {
            var addCommand = _mapper.Map<AddRidePlanCommand>(ridePlanViewModel);
            Bus.SendCommand(addCommand);
        }

        public void Update(RidePlanViewModel ridePlanViewModel)
        {
            var updateCommand = _mapper.Map<UpdateRidePlanCommand>(ridePlanViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveRidePlanCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<RidePlanHistoryData> GetAllHistory(Guid id)
        {
            return RidePlanHistory.ToJavaScriptRidePlanHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<RidePlanViewModel> GetCustomerRidePlans(Guid customerId)
        {
            return _ridePlanRepository.GetCustomerRidePlans(customerId).Select(_mapper.Map<RidePlanViewModel>);
        }

        public IEnumerable<RidePlanViewModel> Get(int fromCityId, int toCityId)
        {
            return _ridePlanRepository.Get(fromCityId, toCityId).Select(_mapper.Map<RidePlanViewModel>);
        }
    }
}
