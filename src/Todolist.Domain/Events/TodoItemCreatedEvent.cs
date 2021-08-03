using Modules.Todolist.Domain.Entities;
using Common.Domain;

namespace Modules.Todolist.Domain.Events
{
    public class TodoItemCreatedEvent : DomainEvent
    {
        public TodoItemCreatedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
