using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Ticket.Application.Interfaces;
using Modules.Workorder.IntegrationEvents.Events;
using Shared.Application.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Ticket.Application.EventHandlers
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
            _logger.LogInformation($"Event: {integrationEvent.GetType().Name} handled by {this.GetType().Name} ");

            var workorder = _context.Workorders.Where(x => x.Id == integrationEvent.WorkorderId).FirstOrDefault();
            workorder.IsClosed = true;

            if (IsLastWorkorderOnTicket(workorder))
                workorder.Ticket.Status = Ticket.Domain.Enums.TicketStatus.Closed;
                
            await _context.SaveChangesAsync(cancellationToken);
        }

        private bool IsLastWorkorderOnTicket(Ticket.Domain.Entities.Workorder workorder)
        {
            return workorder.Ticket.Workorders.Count(x => !x.IsClosed) == 0;
        }
    }
}
