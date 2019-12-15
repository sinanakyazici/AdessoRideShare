using AdessoRideShare.Domain.Models;
using System;
using System.Collections.Generic;

namespace AdessoRideShare.Domain.Interfaces
{
    public interface IRidePlanRepository : IRepository<RidePlan>
    {
        IEnumerable<RidePlan> GetCustomerRidePlans(Guid customerId);
        RidePlan Get(Guid customerId, int fromCityId, int toCityId, DateTime date);
        IEnumerable<RidePlan> Get(int fromCityId, int toCityId);
    }
}
