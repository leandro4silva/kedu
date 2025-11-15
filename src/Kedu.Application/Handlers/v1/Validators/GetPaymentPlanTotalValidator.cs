using FluentValidation;
using Kedu.Application.Handlers.v1.Commands;

namespace Kedu.Application.Handlers.v1.Validators
{
    public class GetPaymentPlanTotalValidator : AbstractValidator<GetPaymentPlanTotalCommand>
    {
        public GetPaymentPlanTotalValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("O ID do plano de pagamento deve ser maior que zero.");
        }
    }
}