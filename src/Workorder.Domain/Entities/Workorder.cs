using Modules.Workorder.Domain.Enums;
using Shared.Domain;
using System.Collections.Generic;

namespace Modules.Workorder.Domain.Entities
{
    public class Workorder : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Activity> Activities { get; set; }
        public PriorityLevel Priority { get; set; }
        public WorkorderStatus Status { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
