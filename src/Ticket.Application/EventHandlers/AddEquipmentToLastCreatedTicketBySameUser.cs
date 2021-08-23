using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Equipment.IntegrationEvents.Events;
using Modules.Ticket.Application.Commands;
using Modules.Ticket.Application.Interfaces;
using Shared.Application.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Ticket.Application.EventHandlers
{
    public class AddEquipmentToLastCreatedTicketBySameUser : INotificationHandler<IntegrationEventNotification<EquipmentCreatedEvent>>
    {
        private readonly ILogger<AddEquipmentToLastCreatedTicketBySameUser> _logger;
        private readonly ITicketDbContext _context;
        private readonly IMediator _mediator;
       

        public AddEquipmentToLastCreatedTicketBySameUser(ILogger<AddEquipmentToLastCreatedTicketBySameUser> logger, ITicketDbContext context, IMediator mediator)
        {
            _logger = logger;
            _context = context;
            _mediator = mediator;
        }
        public async Task Handle(IntegrationEventNotification<EquipmentCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var integrationEvent = notification.IntegrationEvent;
            _logger.LogInformation($"Event: {integrationEvent.GetType().Name} handled by {this.GetType().Name} ");

            var latestCreatedTicket = _context.Tickets.Where(x => x.Status == Ticket.Domain.Enums.TicketStatus.Open && x.CreatedBy == integrationEvent.CreatedBy).OrderByDescending(x => x.Created).FirstOrDefault();
            if (latestCreatedTicket != null)
                await _mediator.Send(new AddEquipmentToTicketCommand() { TicketId = latestCreatedTicket.Id, EquipmentId = integrationEvent.EquipmentId });
        }

    }
    
}
