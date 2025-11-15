using Kedu.Domain.Entities;

namespace Kedu.Domain.Repositories
{
    public interface IPaymentPlanRepository
    {
        Task<PaymentPlan> Get(int id, CancellationToken cancellationToken);
        Task<PaymentPlan> GetByIdWithDetails(int id, CancellationToken cancellationToken);
        Task<List<PaymentPlan>> GetByFinancialManagerId(int financialManagerId, CancellationToken cancellationToken);
        Task Insert(PaymentPlan entity, CancellationToken cancellationToken);
        Task Update(PaymentPlan entity, CancellationToken cancellationToken);
        Task Delete(PaymentPlan entity, CancellationToken cancellationToken);
    }
}