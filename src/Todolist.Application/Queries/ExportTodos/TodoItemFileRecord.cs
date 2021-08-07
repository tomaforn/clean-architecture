using Shared.Application.Mappings;
using Modules.Todolist.Domain.Entities;

namespace Modules.Todolist.Application.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
