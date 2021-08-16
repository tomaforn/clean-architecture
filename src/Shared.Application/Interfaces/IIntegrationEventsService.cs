using Shared.IntegrationEvents;
using System.Threading.Tasks;

namespace Shared.Application.Interfaces
{
    public interface IIntegrationEventService
    {
        Task Publish(IntegrationEvent integrationEvent);
    }
}
