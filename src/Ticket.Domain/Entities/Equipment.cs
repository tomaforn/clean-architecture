using Shared.Domain;
using System.Collections.Generic;

namespace Modules.Ticket.Domain.Entities
{
    public class Equipment : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public Ticket Ticket { get; set; }
        public string MoreEquipmentDetailsRelatedToTicket { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}