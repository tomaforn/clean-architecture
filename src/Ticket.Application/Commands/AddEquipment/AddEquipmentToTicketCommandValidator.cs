using FluentValidation;

namespace Modules.Ticket.Application.Commands
{
    public class AddEquipmentToTicketCommandValidator : AbstractValidator<AddEquipmentToTicketCommand>
    {
        public AddEquipmentToTicketCommandValidator()
        {
            RuleFor(v => v.EquipmentId)
                .NotEmpty();
            
            RuleFor(v => v.TicketId)
                .NotEmpty();
        }
    }
}
