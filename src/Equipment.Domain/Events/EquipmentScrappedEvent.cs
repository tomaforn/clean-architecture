using Modules.Equipment.Domain.Entities;
using Shared.Domain;

namespace Modules.Equipment.Domain.Events
{
    public class EquipmentScrappedEvent : DomainEvent
    {
        public EquipmentScrappedEvent(Entities.Equipment item)
        {
            Item = item;
        }

        public Entities.Equipment Item { get; }
    }
}
