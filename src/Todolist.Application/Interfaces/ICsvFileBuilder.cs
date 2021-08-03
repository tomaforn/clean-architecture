using Modules.Todolist.Application.Queries.ExportTodos;
using System.Collections.Generic;

namespace Modules.Todolist.Application.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
