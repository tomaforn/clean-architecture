using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Equipment.Domain.Events;
using Shared.Application.Interfaces;
using Shared.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Ticket.Application.TodoItems.EventHandlers
{
    public class PublishIntegrationEventWhenEquipmentIsScrapped : INotificationHandler<DomainEventNotification<EquipmentScrappedEvent>>
    {
        private readonly ILogger<PublishIntegrationEventWhenEquipmentIsScrapped> _logger;
        private readonly IIntegrationEventService _integrationEventService;

        public PublishIntegrationEventWhenEquipmentIsScrapped(ILogger<PublishIntegrationEventWhenEquipmentIsScrapped> logger, IIntegrationEventService integrationEventService)
        {
            _logger = logger;
            _integrationEventService = integrationEventService;
        }
        public Task Handle(DomainEventNotification<EquipmentScrappedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _logger.LogInformation($"Event: {domainEvent.GetType().Name} handled by {this.GetType().Name} ");

            var scrappedEvent = new IntegrationEvents.Events.EquipmentScrappedEvent(domainEvent.Item.Id, domainEvent.Item.LastModifiedBy);
            _integrationEventService.Publish(scrappedEvent);

            return Task.CompletedTask;            
        }
    }
}
