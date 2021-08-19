using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Ticket.Domain.Events;
using Shared.Application.Interfaces;
using Shared.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Ticket.Application.TodoItems.EventHandlers
{
    public class PublishIntegrationEventWhenTicketIsClosed : INotificationHandler<DomainEventNotification<TicketClosedEvent>>
    {
        private readonly ILogger<PublishIntegrationEventWhenTicketIsClosed> _logger;
        private readonly IIntegrationEventService _integrationEventService;

        public PublishIntegrationEventWhenTicketIsClosed(ILogger<PublishIntegrationEventWhenTicketIsClosed> logger, IIntegrationEventService integrationEventService)
        {
            _logger = logger;
            _integrationEventService = integrationEventService;
        }
        public Task Handle(DomainEventNotification<TicketClosedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _logger.LogInformation($"Event: {domainEvent.GetType().Name} handled by {this.GetType().Name} ");

            _integrationEventService.Publish(new IntegrationEvents.Events.TicketClosedEvent(domainEvent.Item.Id));

            return Task.CompletedTask;            
        }
    }
}
