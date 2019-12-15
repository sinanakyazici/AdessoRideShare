using System;
using AdessoRideShare.Application.Interfaces;
using AdessoRideShare.Application.ViewModels;
using AdessoRideShare.Domain.Core.Bus;
using AdessoRideShare.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdessoRideShare.Services.Api.Controllers
{
    [ApiController]
    public class CustomerController : ApiController
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomerController(
            ICustomerAppService customerAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _customerAppService = customerAppService;
        }

        [HttpGet]
        [Route("customer-management")]
        public IActionResult Default()
        {
            return Ok();
        }

        [HttpGet]
        [Route("customer-management/all")]
        public IActionResult Get()
        {
            return Response(_customerAppService.GetAll());
        }

        [HttpGet]
        [Route("customer-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var customerViewModel = _customerAppService.GetById(id);
            return Response(customerViewModel);
        }

        [HttpPost]
        [Route("customer-management")]
        public IActionResult Post([FromBody]CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(customerViewModel);
            }

            _customerAppService.Add(customerViewModel);

            return Response(customerViewModel);
        }

        [HttpPut]
        [Route("customer-management")]
        public IActionResult Put([FromBody]CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(customerViewModel);
            }

            _customerAppService.Update(customerViewModel);

            return Response(customerViewModel);
        }

        [HttpDelete]
        [Route("customer-management")]
        public IActionResult Delete(Guid id)
        {
            _customerAppService.Remove(id);

            return Response();
        }

        [HttpGet]
        [Route("customer-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var customerHistoryData = _customerAppService.GetAllHistory(id);
            return Response(customerHistoryData);
        }
    }
}
