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
    public class BookingController : ApiController
    {
        private readonly IBookingAppService _bookingAppService;

        public BookingController(
            IBookingAppService bookingAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _bookingAppService = bookingAppService;
        }

        [HttpGet]
        [Route("booking-management/mybookings/{customerId:guid}")]
        public IActionResult GetCustomerBookings(Guid customerId)
        {
            return Response(_bookingAppService.GetCustomerBookings(customerId));
        }

        [HttpGet]
        [Route("booking-management")]
        public IActionResult Get()
        {
            return Response(_bookingAppService.GetAll());
        }

        [HttpGet]
        [Route("booking-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var viewModel = _bookingAppService.GetById(id);
            return Response(viewModel);
        }

        [HttpPost]
        [Route("booking-management")]
        public IActionResult Post([FromBody]BookingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(viewModel);
            }

            _bookingAppService.Add(viewModel);

            return Response(viewModel);
        }

        [HttpPut]
        [Route("booking-management")]
        public IActionResult Put([FromBody]BookingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(viewModel);
            }

            _bookingAppService.Update(viewModel);

            return Response(viewModel);
        }

        [HttpDelete]
        [Route("booking-management")]
        public IActionResult Delete(Guid id)
        {
            _bookingAppService.Remove(id);
            return Response();
        }

        [HttpGet]
        [Route("booking-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var historyData = _bookingAppService.GetAllHistory(id);
            return Response(historyData);
        }
    }
}