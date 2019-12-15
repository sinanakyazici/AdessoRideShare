using System;
using System.Collections.Generic;
using AdessoRideShare.Application.EventSourcedNormalizers.Customer;
using AdessoRideShare.Application.ViewModels;

namespace AdessoRideShare.Application.Interfaces
{
    public interface ICustomerAppService : IDisposable
    {
        void Add(CustomerViewModel customerViewModel);
        IEnumerable<CustomerViewModel> GetAll();
        CustomerViewModel GetById(Guid id);
        void Update(CustomerViewModel customerViewModel);
        void Remove(Guid id);
        IList<CustomerHistoryData> GetAllHistory(Guid id);
    }
}
