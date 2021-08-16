using MediatR;
using Modules.Ticket.Application.Interfaces;
using Modules.Ticket.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Ticket.Application.Commands
{
    public class CreateTicketCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, int>
    {
        private readonly ITicketDbContext _context;

        public CreateTicketCommandHandler(ITicketDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Ticket
            {
                Name = request.Name
            };

            entity.DomainEvents.Add(new TicketCreatedEvent(entity));

            _context.Tickets.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
