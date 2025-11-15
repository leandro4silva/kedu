using Kedu.Domain.SeedWork;
using Kedu.Domain.Validation;

namespace Kedu.Domain.Entities
{
    public class PaymentPlan : EntityBase
    {
        public decimal TotalValue { get; private set; }

        private readonly List<Billing> _billings = new List<Billing>();
        public IReadOnlyCollection<Billing> Billings => _billings.AsReadOnly();

        public int FinancialManagerId { get; private set; }
        public FinancialManager FinancialManager { get; private set; }

        public int CostCenterId { get; private set; }
        public CostCenter CostCenter { get; private set; }

        public PaymentPlan(decimal totalValue, int financialManagerId, int costCenterId)
        {
            TotalValue = totalValue;
            FinancialManagerId = financialManagerId;
            CostCenterId = costCenterId;

            Validate();
        }

        public void AddBilling(Billing billing)
        {
            DomainValidation.NotNull(billing, nameof(billing));
            _billings.Add(billing);
        }

        private void Validate()
        {
            DomainValidation.GreaterThanZero(TotalValue, nameof(TotalValue));
            DomainValidation.GreaterThanZero(FinancialManagerId, nameof(FinancialManagerId));
            DomainValidation.GreaterThanZero(CostCenterId, nameof(CostCenterId));
        }
    }
}