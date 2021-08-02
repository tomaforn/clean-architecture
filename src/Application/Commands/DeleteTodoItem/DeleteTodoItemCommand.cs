using Common.Application.Exceptions;
using Common.Domain;
using Modules.Todolist.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Modules.Todolist.Application.Interfaces;

namespace Modules.Todolist.Application.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
    {
        private readonly ITodolistDbContext _context;

        public DeleteTodoItemCommandHandler(ITodolistDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            _context.TodoItems.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
