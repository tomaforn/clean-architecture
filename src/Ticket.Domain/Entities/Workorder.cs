using Shared.Domain;
using System.Collections.Generic;

namespace Modules.Ticket.Domain.Entities
{
    public class Workorder : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public int WorkorderId { get; set; }
        public bool IsClosed { get; set; }
        public string SomeOtherInformation { get; set; }
        public Ticket Ticket { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}