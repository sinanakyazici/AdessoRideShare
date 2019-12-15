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
    public class RidePlanController : ApiController
    {
        private readonly IRidePlanAppService _ridePlanAppService;

        public RidePlanController(
            IRidePlanAppService ridePlanAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _ridePlanAppService = ridePlanAppService;
        }

        [HttpGet]
        [Route("rideplan-management/myrideplans/{customerId:guid}")]
        public IActionResult GetCustomerRidePlans(Guid customerId)
        {
            return Response(_ridePlanAppService.GetCustomerRidePlans(customerId));
        }

        [HttpGet]
        [Route("rideplan-management/rideplans/{fromCityId}/{toCityId}")]
        public IActionResult GetRidePlans(int fromCityId, int toCityId)
        {
            return Response(_ridePlanAppService.Get(fromCityId, toCityId));
        }

        [HttpGet]
        [Route("rideplan-management")]
        public IActionResult Get()
        {
            return Response(_ridePlanAppService.GetAll());
        }

        [HttpGet]
        [Route("rideplan-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var viewModel = _ridePlanAppService.GetById(id);
            return Response(viewModel);
        }

        [HttpPost]
        [Route("rideplan-management")]
        public IActionResult Post([FromBody]RidePlanViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(viewModel);
            }

            _ridePlanAppService.Add(viewModel);

            return Response(viewModel);
        }

        [HttpPut]
        [Route("rideplan-management")]
        public IActionResult Put([FromBody]RidePlanViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(viewModel);
            }

            _ridePlanAppService.Update(viewModel);

            return Response(viewModel);
        }

        [HttpDelete]
        [Route("rideplan-management")]
        public IActionResult Delete(Guid id)
        {
            _ridePlanAppService.Remove(id);
            return Response();
        }

        [HttpGet]
        [Route("rideplan-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var historyData = _ridePlanAppService.GetAllHistory(id);
            return Response(historyData);
        }
    }
}