using AdessoRideShare.Domain.Interfaces;
using AdessoRideShare.Domain.Models;
using AdessoRideShare.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdessoRideShare.Infrastructure.Data.Repository
{
    public class RidePlanRepository : Repository<RidePlan>, IRidePlanRepository
    {
        public RidePlanRepository(AdessoRideShareContext context)
            : base(context)
        {

        }

        public RidePlan Get(Guid customerId, int fromCityId, int toCityId, DateTime date)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.CustomerId == customerId && c.FromCityId == fromCityId && c.ToCityId == toCityId && c.Date == date);
        }

        public IEnumerable<RidePlan> Get(int fromCityId, int toCityId)
        {
            return DbSet.AsNoTracking().Where(c => c.FromCityId == fromCityId && c.ToCityId == toCityId);
        }

        public IEnumerable<RidePlan> GetCustomerRidePlans(Guid customerId)
        {
            return DbSet.AsNoTracking().Where(c => c.CustomerId == customerId);
        }
    }
}
