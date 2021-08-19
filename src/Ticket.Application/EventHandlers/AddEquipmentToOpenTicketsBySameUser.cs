﻿using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Ticket.Application.Commands;
using Modules.Ticket.Application.Interfaces;
using Modules.Ticket.IntegrationEvents.Events;
using Shared.Application.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Workorder.Application.TodoItems.EventHandlers
{
    public class AddEquipmentToOpenTicketsBySameUser : INotificationHandler<IntegrationEventNotification<EquipmentCreatedEvent>>
    {
        private readonly ILogger<AddEquipmentToOpenTicketsBySameUser> _logger;
        private readonly ITicketDbContext _context;
        private readonly IMediator _mediator;

        public AddEquipmentToOpenTicketsBySameUser(ILogger<AddEquipmentToOpenTicketsBySameUser> logger, ITicketDbContext context, IMediator mediator)
        {
            _logger = logger;
            _context = context;
            _mediator = mediator;
        }
        public async Task Handle(IntegrationEventNotification<EquipmentCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var integrationEvent = notification.IntegrationEvent;
            _logger.LogInformation($"Event: {integrationEvent.GetType().Name} handled by {this.GetType().Name} ");

            var tickets = _context.Tickets.Where(x => x.Status == Ticket.Domain.Enums.TicketStatus.Open && x.CreatedBy == integrationEvent.CreatedBy).ToList();
            await Task.WhenAll(tickets.Select(async i => await _mediator.Send(new AddEquipmentToTicketCommand() { TicketId = i.Id, EquipmentId = integrationEvent.EquipmentId }, cancellationToken)));
        }

    }
    
}