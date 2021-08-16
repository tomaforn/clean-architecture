using Shared.IntegrationEvents;

namespace Modules.Workorder.IntegrationEvents.Events
{
    public class WorkorderCreatedEvent : IntegrationEvent
    {
        public WorkorderCreatedEvent(int workorderId)
        {
            WorkorderId = workorderId;
        }

        public int WorkorderId { get; }
    }
}
