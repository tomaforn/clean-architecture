using Modules.Workorder.Domain.Entities;
using Shared.Domain;

namespace Modules.Workorder.Domain.Events
{
    public class WorkorderClosedEvent : DomainEvent
    {
        public WorkorderClosedEvent(Entities.Workorder item)
        {
            Item = item;
        }

        public Entities.Workorder Item { get; }
    }
}
