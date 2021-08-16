using Modules.Workorder.Domain.Entities;
using Shared.Domain;

namespace Modules.Workorder.Domain.Events
{
    public class WorkorderCreatedEvent : DomainEvent
    {
        public WorkorderCreatedEvent(Entities.Workorder item)
        {
            Item = item;
        }

        public Entities.Workorder Item { get; }
    }
}
