using AdessoRideShare.Domain.Interfaces;
using AdessoRideShare.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AdessoRideShare.Infrastructure.Data.Repository
{
    public class CustomerRepository : Repository<Domain.Models.Customer>, ICustomerRepository
    {
        public CustomerRepository(AdessoRideShareContext context)
            : base(context)
        {

        }

        public Domain.Models.Customer GetByEmail(string email)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        }
    }
}
