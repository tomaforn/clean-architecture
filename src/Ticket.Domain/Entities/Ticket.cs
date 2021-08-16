using Modules.Ticket.Domain.Enums;
using Modules.Ticket.Domain.Events;
using Shared.Domain;
using System.Collections.Generic;

namespace Modules.Ticket.Domain.Entities
{
    public class Ticket : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TicketType Type { get; set; }
        private TicketStatus _status;
        public TicketStatus Status
        {
            get => _status;
            set
            {
                if (value == TicketStatus.Closed && _status != TicketStatus.Closed)
                {
                    DomainEvents.Add(new TicketClosedEvent(this));
                }

                _status = value;
            }
        }
        public List<Workorder> TicketWorkorders { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}