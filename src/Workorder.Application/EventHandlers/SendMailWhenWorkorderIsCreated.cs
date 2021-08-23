using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Workorder.Domain.Events;
using Shared.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Workorder.Application.EventHandlers
{
    public class SendMailWhenWorkorderIsCreated : INotificationHandler<DomainEventNotification<WorkorderCreatedEvent>>
    {
        private readonly ILogger<SendMailWhenWorkorderIsCreated> _logger;

        public SendMailWhenWorkorderIsCreated(ILogger<SendMailWhenWorkorderIsCreated> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<WorkorderCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            _logger.LogInformation($"Event: {domainEvent.GetType().Name} handled by {this.GetType().Name} ");

            //TODO Inject IMailService, and send mail here..

            return Task.CompletedTask;
        }
    }
}
