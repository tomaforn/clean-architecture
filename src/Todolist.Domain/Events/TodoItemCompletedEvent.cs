using Modules.Todolist.Domain.Entities;
using Shared.Domain;

namespace Modules.Todolist.Domain.Events
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
