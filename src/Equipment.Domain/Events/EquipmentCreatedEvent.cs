using Modules.Equipment.Domain.Entities;
using Shared.Domain;

namespace Modules.Equipment.Domain.Events
{
    public class EquipmentCreatedEvent : DomainEvent
    {
        public EquipmentCreatedEvent(Entities.Equipment item)
        {
            Item = item;
        }

        public Entities.Equipment Item { get; }
    }
}
