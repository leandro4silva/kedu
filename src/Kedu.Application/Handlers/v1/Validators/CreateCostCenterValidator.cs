using FluentValidation;
using Kedu.Application.Handlers.v1.Commands;

namespace Kedu.Application.Handlers.v1.Validators
{
    public class CreateCostCenterValidator : AbstractValidator<CreateCostCenterCommand>
    {
        public CreateCostCenterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome do centro de custo é obrigatório.")
                .MaximumLength(100)
                .WithMessage("O nome do centro de custo não pode exceder 100 caracteres.")
                .WithName("nome");
        }
    }
}