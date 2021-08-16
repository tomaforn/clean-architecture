using Shared.IntegrationEvents;

namespace Modules.Ticket.IntegrationEvents.Events
{
    public class TicketClosedEvent : IntegrationEvent
    {
        public TicketClosedEvent(int ticketId)
        {
            TicketId = ticketId;
        }

        public int TicketId { get; }
    }
}
