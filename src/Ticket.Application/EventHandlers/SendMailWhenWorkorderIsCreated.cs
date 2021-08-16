using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Ticket.Domain.Events;
using Shared.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Ticket.Application.TodoItems.EventHandlers
{
    public class SendMailWhenTicketIsCreated : INotificationHandler<DomainEventNotification<TicketCreatedEvent>>
    {
        private readonly ILogger<SendMailWhenTicketIsCreated> _logger;

        public SendMailWhenTicketIsCreated(ILogger<SendMailWhenTicketIsCreated> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<TicketCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent} - Running SendMailWhenTicketIsCreated", domainEvent.GetType().Name);

            //TODO Inject IMailService, and send mail here..

            return Task.CompletedTask;
        }
    }
}
