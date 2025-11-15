using Kedu.Domain.Enums;
using Kedu.Domain.Exceptions;
using Kedu.Domain.SeedWork;
using Kedu.Domain.Validation;

namespace Kedu.Domain.Entities
{
    public class Billing : EntityBase
    {
        public decimal Value { get; private set; }
        public DateTime DueDate { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public StatusBilling StatusBilling { get; private set; }
        public string? PaymentCode { get; private set; }
        public int PaymentPlanId { get; private set; }
        public PaymentPlan PaymentPlan { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public decimal? PaidValue { get; private set; }

        public Billing(
            decimal value,
            DateTime dueDate,
            PaymentMethod paymentMethod,
            int paymentPlanId
        )
        {
            Value = value;
            DueDate = dueDate;
            PaymentMethod = paymentMethod;
            StatusBilling = StatusBilling.Issued;
            PaymentPlanId = paymentPlanId;

            Validate();

            PaymentCode = GeneratePaymentCode(paymentMethod);
        }

        public void Update(
            decimal value,
            DateTime dueDate,
            PaymentMethod paymentMethod
        )
        {
            Value = value;
            DueDate = dueDate;
            PaymentMethod = paymentMethod;

            Validate();
        }

        private void Validate()
        {
            DomainValidation.GreaterThanZero(Value, nameof(Value));
            DomainValidation.NotDefault(DueDate, nameof(DueDate));
            DomainValidation.ValidEnum(PaymentMethod, nameof(PaymentMethod));
            DomainValidation.ValidEnum(StatusBilling, nameof(StatusBilling));
            DomainValidation.GreaterThanZero(PaymentPlanId, nameof(PaymentPlanId));
        }

        public void RegisterPayment(decimal paidValue, DateTime paymentDate)
        {
            if (StatusBilling == StatusBilling.Paid)
            {
                throw new EntityValidationException("Esta cobrança já foi paga.");
            }

            if (StatusBilling == StatusBilling.Cancelled)
            {
                throw new EntityValidationException("Não é possível registrar pagamento em uma cobrança cancelada.");
            }

            if (paymentDate > DateTime.Now)
            {
                throw new EntityValidationException("A data de pagamento não pode ser futura.");
            }

            if (paidValue <= 0)
            {
                throw new EntityValidationException("O valor pago deve ser maior que zero.");
            }

            PaidValue = paidValue;
            PaymentDate = paymentDate;

            if (paidValue == Value)
            {
                StatusBilling = StatusBilling.Paid;
            }
            else if (paidValue < Value)
            {
                throw new EntityValidationException("Apenas pagamentos totais são permitidos no momento.");
            }
            else
            {
                throw new EntityValidationException("O valor pago não pode ser maior que o valor da cobrança.");
            }

            SetModified();
        }

        private string GeneratePaymentCode(PaymentMethod method)
        {
            return method switch
            {
                PaymentMethod.Boleto => GenerateFakeBoletoLine(),
                PaymentMethod.Pix => GenerateFakePixCode()
            };
        }

        private string GenerateFakeBoletoLine()
        {
            return $"{RandomNumber(5)}{RandomNumber(5)}{RandomNumber(5)}{RandomNumber(5)}{RandomNumber(5)}{RandomNumber(5)}{RandomNumber(17)}";
        }

        private string GenerateFakePixCode()
        {
            return $"PIX-{Guid.NewGuid()}";
        }

        private string RandomNumber(int length)
        {
            var rnd = new Random();
            return string.Concat(Enumerable.Range(0, length).Select(_ => rnd.Next(0, 10)));
        }
    }
}
