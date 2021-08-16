using Shared.Application.Mappings;
using System.Reflection;

namespace Todolist.Application.Mappings
{
    public class WorkorderMappingProfile : MappingProfile 
    {
        public WorkorderMappingProfile() : base(Assembly.GetExecutingAssembly()) { }
    }
}
