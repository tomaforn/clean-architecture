using Shared.Domain;
using System.Collections.Generic;

namespace User.Domain.Entities
{
    public class UserDetails : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public int Shoesize { get; set; }       
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
