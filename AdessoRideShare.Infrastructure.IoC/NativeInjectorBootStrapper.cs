using AdessoRideShare.Application.Interfaces;
using AdessoRideShare.Application.Services;
using AdessoRideShare.Domain.CommandHandlers;
using AdessoRideShare.Domain.Commands.Booking;
using AdessoRideShare.Domain.Commands.Customer;
using AdessoRideShare.Domain.Commands.RidePlan;
using AdessoRideShare.Domain.Core.Bus;
using AdessoRideShare.Domain.Core.Events;
using AdessoRideShare.Domain.Core.Notifications;
using AdessoRideShare.Domain.EventHandlers;
using AdessoRideShare.Domain.Events.Booking;
using AdessoRideShare.Domain.Events.Customer;
using AdessoRideShare.Domain.Events.RidePlan;
using AdessoRideShare.Domain.Interfaces;
using AdessoRideShare.Infrastructure.Bus;
using AdessoRideShare.Infrastructure.Data.Context;
using AdessoRideShare.Infrastructure.Data.EventSourcing;
using AdessoRideShare.Infrastructure.Data.Repository;
using AdessoRideShare.Infrastructure.Data.Repository.EventSourcing;
using AdessoRideShare.Infrastructure.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AdessoRideShare.Infrastructure.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            services.AddScoped<IRidePlanAppService, RidePlanAppService>();
            services.AddScoped<IBookingAppService, BookingAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<CustomerAddedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<RidePlanAddedEvent>,   RidePlanEventHandler>();
            services.AddScoped<INotificationHandler<RidePlanUpdatedEvent>, RidePlanEventHandler>();
            services.AddScoped<INotificationHandler<RidePlanRemovedEvent>, RidePlanEventHandler>();
            services.AddScoped<INotificationHandler<BookingAddedEvent>,   BookingEventHandler>();
            services.AddScoped<INotificationHandler<BookingUpdatedEvent>, BookingEventHandler>();
            services.AddScoped<INotificationHandler<BookingRemovedEvent>, BookingEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<AddCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<AddRidePlanCommand, bool>, RidePlanCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateRidePlanCommand, bool>, RidePlanCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveRidePlanCommand, bool>, RidePlanCommandHandler>();
            services.AddScoped<IRequestHandler<AddBookingCommand, bool>, BookingCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateBookingCommand, bool>, BookingCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveBookingCommand, bool>, BookingCommandHandler>();

            // Infra - Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IRidePlanRepository, RidePlanRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<AdessoRideShareContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();
        }
    }
}
