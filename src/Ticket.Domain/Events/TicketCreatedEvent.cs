using Modules.Ticket.Domain.Entities;
using Shared.Domain;

namespace Modules.Ticket.Domain.Events
{
    public class TicketCreatedEvent : DomainEvent
    {
        public TicketCreatedEvent(Entities.Ticket item)
        {
            Item = item;
        }

        public Entities.Ticket Item { get; }
    }
}
