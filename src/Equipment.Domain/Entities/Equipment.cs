using Modules.Equipment.Domain.Enums;
using Modules.Equipment.Domain.Events;
using Shared.Domain;
using System.Collections.Generic;

namespace Modules.Equipment.Domain.Entities
{
    public class Equipment : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        private EquipmentStatus _status;
        public EquipmentStatus Status
        {
            get => _status;
            set
            {
                if (value == EquipmentStatus.Scrapped && _status != EquipmentStatus.Scrapped)
                {
                    DomainEvents.Add(new EquipmentScrappedEvent(this));
                }

                _status = value;
            }
        }
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}