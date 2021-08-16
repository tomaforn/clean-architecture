using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Ticket.Application.Interfaces;
using Modules.Workorder.IntegrationEvents.Events;
using Shared.Application.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Workorder.Application.TodoItems.EventHandlers
{
    public class CloseTicketWhenLastWorkorderIsClosed : INotificationHandler<IntegrationEventNotification<WorkorderClosedEvent>>
    {
        private readonly ILogger<CloseTicketWhenLastWorkorderIsClosed> _logger;
        private readonly ITicketDbContext _context;

        public CloseTicketWhenLastWorkorderIsClosed(ILogger<CloseTicketWhenLastWorkorderIsClosed> logger, ITicketDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Handle(IntegrationEventNotification<WorkorderClosedEvent> notification, CancellationToken cancellationToken)
        {
            var integrationEvent = notification.IntegrationEvent;
            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent} - Running CloseTicketWhenLastWorkorderIsClosed", integrationEvent.GetType().Name);

            var ticketWorkorder = _context.TicketWorkorders.Where(x => x.WorkorderId == integrationEvent.WorkorderId).FirstOrDefault();
            ticketWorkorder.IsClosed = true;

            if (IsLastWorkorderOnTicket(ticketWorkorder))
                ticketWorkorder.Ticket.Status = Ticket.Domain.Enums.TicketStatus.Closed;
                
            await _context.SaveChangesAsync(cancellationToken);
        }

        private bool IsLastWorkorderOnTicket(Ticket.Domain.Entities.Workorder workorder)
        {
            return workorder.Ticket.TicketWorkorders.Count(x => !x.IsClosed) == 0;
        }
    }
}
