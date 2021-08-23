using MediatR;
using Modules.Ticket.Application.Interfaces;
using Modules.Ticket.Domain.Events;
using Shared.Application.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Ticket.Application.Commands
{
    public class AddEquipmentToTicketCommand : IRequest<int>
    {
        public int TicketId { get; set; }
        public int EquipmentId { get; set; }
    }

    public class AddEquipmentToTicketCommandHandler : IRequestHandler<AddEquipmentToTicketCommand,int>
    {
        private readonly ITicketDbContext _context;

        public AddEquipmentToTicketCommandHandler(ITicketDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddEquipmentToTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _context.Tickets.FindAsync(request.TicketId);
            if(ticket == null)
                throw new NotFoundException(nameof(Domain.Entities.Ticket), request.TicketId);

            var entity = new Domain.Entities.Equipment
            {
                EquipmentId = request.EquipmentId,
                Ticket = ticket
            };

            _context.Equipments.Add(entity);
            return await _context.SaveChangesAsync(cancellationToken);            
        }

    }
}
