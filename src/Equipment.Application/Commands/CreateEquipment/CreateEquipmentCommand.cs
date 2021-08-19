using MediatR;
using Modules.Equipment.Application.Interfaces;
using Modules.Equipment.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Equipment.Application.Commands
{
    public class CreateEquipmentCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateEquipmentCommandHandler : IRequestHandler<CreateEquipmentCommand, int>
    {
        private readonly IEquipmentDbContext _context;

        public CreateEquipmentCommandHandler(IEquipmentDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Equipment
            {
                Name = request.Name
            };

            entity.DomainEvents.Add(new EquipmentCreatedEvent(entity));
            _context.Equipment.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
