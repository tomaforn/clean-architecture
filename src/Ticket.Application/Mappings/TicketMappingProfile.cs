using Shared.Application.Mappings;
using System.Reflection;

namespace Modules.Ticket.Application.Mappings
{
    public class TicketMappingProfile : MappingProfile 
    {
        public TicketMappingProfile() : base(Assembly.GetExecutingAssembly()) { }
    }
}
