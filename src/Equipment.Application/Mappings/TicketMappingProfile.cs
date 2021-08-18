using Shared.Application.Mappings;
using System.Reflection;

namespace Todolist.Application.Mappings
{
    public class TicketMappingProfile : MappingProfile 
    {
        public TicketMappingProfile() : base(Assembly.GetExecutingAssembly()) { }
    }
}
