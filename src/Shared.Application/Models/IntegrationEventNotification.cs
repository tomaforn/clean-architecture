using MediatR;
using Shared.IntegrationEvents;

namespace Shared.Application.Models
{
    public class IntegrationEventNotification<TIntegrationEvent> : INotification where TIntegrationEvent : IntegrationEvent
    {
        public IntegrationEventNotification(TIntegrationEvent integrationEvent)
        {
            IntegrationEvent = integrationEvent;
        }

        public TIntegrationEvent IntegrationEvent { get; }
    }
}
