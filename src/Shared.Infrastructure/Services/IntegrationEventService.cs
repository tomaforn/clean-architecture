using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Application.Interfaces;
using Shared.Application.Models;
using System;
using System.Threading.Tasks;

namespace Shared.IntegrationEvents.Services
{
    public class IntegrationEventService : IIntegrationEventService
    {
        private readonly ILogger<IntegrationEventService> _logger;
        private readonly IPublisher _mediator;

        public IntegrationEventService(ILogger<IntegrationEventService> logger, IPublisher mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Publish(IntegrationEvent integrationEvent)
        {
            _logger.LogInformation("Publishing integration event. Event - {event}", integrationEvent.GetType().Name);
            await _mediator.Publish(GetNotificationCorrespondingToIntegrationEvent(integrationEvent));

            throw new NotImplementedException();
        }

        private INotification GetNotificationCorrespondingToIntegrationEvent(IntegrationEvent integrationEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(IntegrationEventNotification<>).MakeGenericType(integrationEvent.GetType()), integrationEvent);
        }
    }
}