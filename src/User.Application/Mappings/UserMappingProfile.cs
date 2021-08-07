using Common.Application.Mappings;
using System.Reflection;

namespace Todolist.Application.Mappings
{
    public class UserMappingProfile : MappingProfile 
    {
        public UserMappingProfile() : base(Assembly.GetExecutingAssembly()) { }
    }
}
