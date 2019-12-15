using AdessoRideShare.Application.EventSourcedNormalizers.RidePlan;
using AdessoRideShare.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace AdessoRideShare.Application.Interfaces
{
    public interface IRidePlanAppService : IDisposable
    {
        void Add(RidePlanViewModel ridePlanViewModel);
        IEnumerable<RidePlanViewModel> GetAll();
        RidePlanViewModel GetById(Guid id);
        void Update(RidePlanViewModel ridePlanViewModel);
        void Remove(Guid id);

        IList<RidePlanHistoryData> GetAllHistory(Guid id);
        IEnumerable<RidePlanViewModel> GetCustomerRidePlans(Guid customerId);
        IEnumerable<RidePlanViewModel> Get(int fromCityId, int toCityId);
    }
}
