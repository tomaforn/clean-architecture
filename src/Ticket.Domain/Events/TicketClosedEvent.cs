using Modules.Ticket.Domain.Entities;
using Shared.Domain;

namespace Modules.Ticket.Domain.Events
{
    public class TicketClosedEvent : DomainEvent
    {
        public TicketClosedEvent(Entities.Ticket item)
        {
            Item = item;
        }

        public Entities.Ticket Item { get; }
    }
}
