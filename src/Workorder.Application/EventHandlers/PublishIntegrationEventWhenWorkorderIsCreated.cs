using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Workorder.Domain.Events;
using Shared.Application.Interfaces;
using Shared.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Workorder.Application.TodoItems.EventHandlers
{
    public class PublishIntegrationEventWhenWorkorderIsCreated : INotificationHandler<DomainEventNotification<WorkorderCreatedEvent>>
    {
        private readonly ILogger<PublishIntegrationEventWhenWorkorderIsCreated> _logger;
        private readonly IIntegrationEventService _integrationEventService;

        public PublishIntegrationEventWhenWorkorderIsCreated(ILogger<PublishIntegrationEventWhenWorkorderIsCreated> logger, IIntegrationEventService integrationEventService)
        {
            _logger = logger;
            _integrationEventService = integrationEventService;
        }

        public Task Handle(DomainEventNotification<WorkorderCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent} - Running PublishIntegrationEventWhenWorkorderIsCreated", domainEvent.GetType().Name);
            _integrationEventService.Publish(new IntegrationEvents.Events.WorkorderCreatedEvent(domainEvent.Item.Id));

            return Task.CompletedTask;
        }
    }
}
