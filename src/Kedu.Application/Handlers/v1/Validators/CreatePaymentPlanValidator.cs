using FluentValidation;
using Kedu.Application.Handlers.v1.Commands;

namespace Kedu.Application.Handlers.v1.Validators
{
    public class CreatePaymentPlanValidator : AbstractValidator<CreatePaymentPlanCommand>
    {
        public CreatePaymentPlanValidator()
        {
            RuleFor(x => x.FinancialManagerId)
                .GreaterThan(0)
                .WithName("responsavelId")
                .WithMessage("O ID do responsável deve ser maior que zero.");

            RuleFor(x => x.CostCenterId)
                .GreaterThan(0)
                .WithName("centroDeCusto")
                .WithMessage("O ID do centro de custo deve ser maior que zero.");

            RuleFor(x => x.Billings)
                .NotEmpty()
                .WithMessage("Deve haver pelo menos uma cobrança.");

            RuleForEach(x => x.Billings).SetValidator(new BillingItemValidator());
        }
    }

    public class BillingItemValidator : AbstractValidator<BillingItem>
    {
        public BillingItemValidator()
        {
            RuleFor(x => x.Value)
                .GreaterThan(0)
                .WithMessage("O valor da cobrança deve ser maior que zero.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.Now.Date)
                .WithMessage("A data de vencimento deve ser futura.");

            RuleFor(x => x.PaymentMethod)
                .IsInEnum()
                .WithMessage("Método de pagamento inválido.");
        }
    }
}