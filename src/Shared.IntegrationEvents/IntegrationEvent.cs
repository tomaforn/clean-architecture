using System;

namespace Shared.IntegrationEvents
{
    public abstract class IntegrationEvent
    {
        protected IntegrationEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }
        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
