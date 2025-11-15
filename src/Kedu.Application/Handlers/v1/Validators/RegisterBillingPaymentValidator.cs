using FluentValidation;
using Kedu.Application.Handlers.v1.Commands;

namespace Kedu.Application.Handlers.v1.Validators
{
    public class RegisterBillingPaymentValidator : AbstractValidator<RegisterBillingPaymentCommand>
    {
        public RegisterBillingPaymentValidator()
        {
            RuleFor(x => x.BillingId)
                .GreaterThan(0)
                .WithMessage("O ID da cobrança deve ser maior que zero.");

            RuleFor(x => x.Value)
                .GreaterThan(0)
                .WithMessage("O valor do pagamento deve ser maior que zero.");

            RuleFor(x => x.PaymentDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("A data de pagamento não pode ser futura.");
        }
    }
}