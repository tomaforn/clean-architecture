using Shared.Application.Mappings;
using System.Reflection;

namespace Todolist.Application.Mappings
{
    public class TodoListMappingProfile : MappingProfile 
    {
        public TodoListMappingProfile() : base(Assembly.GetExecutingAssembly()) { }
    }
}
