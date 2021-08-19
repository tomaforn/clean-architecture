using MediatR;
using Modules.Ticket.Application.Interfaces;
using Modules.Ticket.Domain.Events;
using Shared.Application.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Ticket.Application.Commands
{
    public class AddEquipmentToTicketCommand : IRequest<Unit>
    {
        public int TicketId { get; set; }
        public int EquipmentId { get; set; }
    }

    public class AddEquipmentToTicketCommandHandler : IRequestHandler<AddEquipmentToTicketCommand, Unit>
    {
        private readonly ITicketDbContext _context;

        public AddEquipmentToTicketCommandHandler(ITicketDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddEquipmentToTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.FindAsync(request.TicketId);
            if(ticket == null)
                throw new NotFoundException(nameof(Domain.Entities.Ticket), request.TicketId);

            var entity = new Domain.Entities.Equipment
            {
                Id = request.EquipmentId,
                Ticket = ticket
            };

            _context.Equipments.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
