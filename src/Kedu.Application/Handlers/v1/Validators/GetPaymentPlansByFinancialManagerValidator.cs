using FluentValidation;
using Kedu.Application.Handlers.v1.Commands;

namespace Kedu.Application.Handlers.v1.Validators
{
    public class GetPaymentPlansByFinancialManagerValidator : AbstractValidator<GetPaymentPlansByFinancialManagerCommand>
    {
        public GetPaymentPlansByFinancialManagerValidator()
        {
            RuleFor(x => x.FinancialManagerId)
                .GreaterThan(0)
                .WithMessage("O ID do responsável deve ser maior que zero.");
        }
    }
}