
using FluentValidation;
using Kedu.Application.Handlers.v1.Commands;

namespace Kedu.Application.Handlers.v1.Validators
{
    public class GetBillingsByFinancialManagerValidator : AbstractValidator<GetBillingsByFinancialManagerCommand>
    {
        public GetBillingsByFinancialManagerValidator()
        {
            RuleFor(x => x.FinancialManagerId)
                .GreaterThan(0)
                .WithMessage("O ID do responsável deve ser maior que zero.");

            When(x => x.StartDate.HasValue && x.EndDate.HasValue, () =>
            {
                RuleFor(x => x.EndDate)
                    .GreaterThanOrEqualTo(x => x.StartDate)
                    .WithMessage("A data fim deve ser maior ou igual à data início.");
            });
        }
    }
}