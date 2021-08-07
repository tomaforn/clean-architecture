using Common.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Todolist.Application.Mappings
{
    public class TodoListMappingProfile : MappingProfile 
    {
        public TodoListMappingProfile() : base(Assembly.GetExecutingAssembly()) { }
    }
}
