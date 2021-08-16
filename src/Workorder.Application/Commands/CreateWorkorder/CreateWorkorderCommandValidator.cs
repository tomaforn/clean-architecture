using FluentValidation;

namespace Modules.Workorder.Application.Commands
{
    public class CreateWorkorderCommandValidator : AbstractValidator<CreateWorkorderCommand>
    {
        public CreateWorkorderCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
