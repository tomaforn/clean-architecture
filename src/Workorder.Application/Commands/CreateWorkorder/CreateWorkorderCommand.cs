using MediatR;
using Modules.Workorder.Application.Interfaces;
using Modules.Workorder.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Workorder.Application.Commands
{
    public class CreateWorkorderCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateWorkorderCommandHandler : IRequestHandler<CreateWorkorderCommand, int>
    {
        private readonly IWorkorderDbContext _context;

        public CreateWorkorderCommandHandler(IWorkorderDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateWorkorderCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Workorder
            {
                Name = request.Name
            };

            entity.DomainEvents.Add(new WorkorderCreatedEvent(entity));

            _context.Workorders.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
