using Common.Application.Exceptions;
using Common.Application.Interfaces;
using Modules.Todolist.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Modules.Todolist.Application.Interfaces;

namespace Modules.Todolist.Application.Commands.UpdateTodoList
{
    public class UpdateTodoListCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }

    public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
    {
        private readonly ITodolistDbContext _context;

        public UpdateTodoListCommandHandler(ITodolistDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoLists.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoList), request.Id);
            }

            entity.Title = request.Title;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
