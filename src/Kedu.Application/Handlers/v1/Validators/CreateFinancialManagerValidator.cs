using FluentValidation;
using Kedu.Application.Handlers.v1.Commands;

namespace Kedu.Application.Handlers.v1.Validators
{
    public class CreateFinancialManagerValidator : AbstractValidator<CreateFinancialManagerCommand>
    {
        public CreateFinancialManagerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("O nome do responsável deve ser informado.");
        }
    }
}
