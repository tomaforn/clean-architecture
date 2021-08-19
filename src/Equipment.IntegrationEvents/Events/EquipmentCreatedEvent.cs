using Shared.IntegrationEvents;

namespace Modules.Ticket.IntegrationEvents.Events
{
    public class EquipmentCreatedEvent : IntegrationEvent
    {
        public EquipmentCreatedEvent(int equipmentId, string user)
        {
            EquipmentId = equipmentId;
            CreatedBy = user;
        }

        public int EquipmentId { get; }
        public string CreatedBy { get; }
    }
}
