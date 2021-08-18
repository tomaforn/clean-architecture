using Shared.IntegrationEvents;

namespace Modules.Equipment.IntegrationEvents.Events
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
