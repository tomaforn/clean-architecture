using FluentValidation;

namespace Modules.Equipment.Application.Commands
{
    public class ScrapEquipmentCommandValidator : AbstractValidator<ScrapEquipmentCommand>
    {
        public ScrapEquipmentCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();
        }
    }
}
