using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Equipment.Domain.Events;
using Shared.Application.Interfaces;
using Shared.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Ticket.Application.TodoItems.EventHandlers
{
    public class PublishIntegrationEventWhenEquipmentIsCreated : INotificationHandler<DomainEventNotification<EquipmentCreatedEvent>>
    {
        private readonly ILogger<PublishIntegrationEventWhenEquipmentIsCreated> _logger;
        private readonly IIntegrationEventService _integrationEventService;

        public PublishIntegrationEventWhenEquipmentIsCreated(ILogger<PublishIntegrationEventWhenEquipmentIsCreated> logger, IIntegrationEventService integrationEventService)
        {
            _logger = logger;
            _integrationEventService = integrationEventService;
        }
        public async Task Handle(DomainEventNotification<EquipmentCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _logger.LogInformation($"Event: {domainEvent.GetType().Name} handled by {this.GetType().Name} ");
            await _integrationEventService.Publish(new IntegrationEvents.Events.EquipmentCreatedEvent(domainEvent.Item.Id, domainEvent.Item.CreatedBy));
        }
    }
}
