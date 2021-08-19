using Shared.Application.Mappings;
using System.Reflection;

namespace Modules.Equipment.Application.Mappings
{
    public class EquipmentMappingProfile : MappingProfile 
    {
        public EquipmentMappingProfile() : base(Assembly.GetExecutingAssembly()) { }
    }
}
