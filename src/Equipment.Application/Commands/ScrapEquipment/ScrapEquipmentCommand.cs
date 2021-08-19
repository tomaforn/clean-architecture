using MediatR;
using Modules.Equipment.Application.Interfaces;
using Modules.Equipment.Domain.Events;
using Shared.Application.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Equipment.Application.Commands
{
    public class ScrapEquipmentCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class ScrapEquipmentCommandHandler : IRequestHandler<ScrapEquipmentCommand>
    {
        private readonly IEquipmentDbContext _context;

        public ScrapEquipmentCommandHandler(IEquipmentDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ScrapEquipmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Equipment.FindAsync(request.Id);
            if (entity == null)
                throw new NotFoundException(nameof(Domain.Entities.Equipment), request.Id);
            
            entity.Status = Domain.Enums.EquipmentStatus.Scrapped;
            entity.DomainEvents.Add(new EquipmentScrappedEvent(entity));
            
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
