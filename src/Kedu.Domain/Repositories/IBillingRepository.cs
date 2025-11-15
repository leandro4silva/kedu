using Kedu.Domain.Entities;
using Kedu.Domain.Enums;
using Kedu.Domain.SeedWork;

namespace Kedu.Domain.Repositories
{
    public interface IBillingRepository : IGenericRepository<Billing>
    {
        Task<List<Billing>> GetByFinancialManagerId(int financialManagerId, StatusBilling? status, PaymentMethod? paymentMethod, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken);
        Task<int> GetCountByFinancialManagerId(int financialManagerId, StatusBilling? status, PaymentMethod? paymentMethod, CancellationToken cancellationToken);
    }
}
