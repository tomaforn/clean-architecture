using Shared.Application.Mappings;
using System.Reflection;

namespace Modules.Workorder.Application.Mappings
{
    public class WorkorderMappingProfile : MappingProfile 
    {
        public WorkorderMappingProfile() : base(Assembly.GetExecutingAssembly()) { }
    }
}
