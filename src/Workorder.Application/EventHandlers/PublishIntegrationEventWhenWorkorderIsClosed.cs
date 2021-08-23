using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Workorder.Domain.Events;
using Shared.Application.Interfaces;
using Shared.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Workorder.Application.EventHandlers
{
    public class PublishIntegrationEventWhenWorkorderIsClosed : INotificationHandler<DomainEventNotification<WorkorderClosedEvent>>
    {
        private readonly ILogger<PublishIntegrationEventWhenWorkorderIsClosed> _logger;
        private readonly IIntegrationEventService _integrationEventService;

        public PublishIntegrationEventWhenWorkorderIsClosed(ILogger<PublishIntegrationEventWhenWorkorderIsClosed> logger, IIntegrationEventService integrationEventService)
        {
            _logger = logger;
            _integrationEventService = integrationEventService;
        }

        public Task Handle(DomainEventNotification<WorkorderClosedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _logger.LogInformation($"Event: {domainEvent.GetType().Name} handled by {this.GetType().Name} ");
            
            _integrationEventService.Publish(new IntegrationEvents.Events.WorkorderCreatedEvent(domainEvent.Item.Id));

            return Task.CompletedTask;
        }
    }
}
