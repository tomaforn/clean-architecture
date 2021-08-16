using Shared.IntegrationEvents;

namespace Modules.Workorder.IntegrationEvents.Events
{
    public class WorkorderClosedEvent : IntegrationEvent
    {
        public WorkorderClosedEvent(int workorderId)
        {
            WorkorderId = workorderId;
        }

        public int WorkorderId { get; }
    }
}
