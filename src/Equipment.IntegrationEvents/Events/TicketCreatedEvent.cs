using Shared.IntegrationEvents;

namespace Modules.Equipment.IntegrationEvents.Events
{
    public class TicketCreatedEvent : IntegrationEvent
    {
        public TicketCreatedEvent(int ticketId)
        {
            TicketId = ticketId;
        }

        public int TicketId { get; }
    }
}
