using Common.Domain;
using System.Threading.Tasks;

namespace Common.Application.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
