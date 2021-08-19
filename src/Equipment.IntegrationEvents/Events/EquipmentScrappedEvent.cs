using Shared.IntegrationEvents;

namespace Modules.Ticket.IntegrationEvents.Events
{
    public class EquipmentScrappedEvent : IntegrationEvent
    {
        public EquipmentScrappedEvent(int equipmentId, string user)
        {
            EquipmentId = equipmentId;
            ScrappedBy = user;
        }

        public int EquipmentId { get; }
        public string ScrappedBy { get; }
    }
}
