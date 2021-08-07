using Shared.Domain;
using System.Threading.Tasks;

namespace Shared.Application.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
