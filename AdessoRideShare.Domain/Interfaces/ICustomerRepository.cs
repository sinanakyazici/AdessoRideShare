using AdessoRideShare.Domain.Models;

namespace AdessoRideShare.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);
    }
}