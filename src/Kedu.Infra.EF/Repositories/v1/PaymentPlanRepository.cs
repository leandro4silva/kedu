using Kedu.Domain.Entities;
using Kedu.Domain.Repositories;
using Kedu.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Kedu.Infra.Data.EF.Repositories.v1
{
    public class PaymentPlanRepository : IPaymentPlanRepository
    {
        private readonly KeduDbContext _context;

        public PaymentPlanRepository(KeduDbContext context)
        {
            _context = context;
        }

        public async Task Delete(PaymentPlan entity, CancellationToken cancellationToken)
        {
            _context.PaymentPlans.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PaymentPlan> Get(int id, CancellationToken cancellationToken)
        {
            return await _context.PaymentPlans
                .FirstOrDefaultAsync(pp => pp.Id == id, cancellationToken);
        }

        public async Task<PaymentPlan> GetByIdWithDetails(int id, CancellationToken cancellationToken)
        {
            return await _context.PaymentPlans
                .Include(pp => pp.FinancialManager)
                .Include(pp => pp.CostCenter)
                .Include(pp => pp.Billings)
                .FirstOrDefaultAsync(pp => pp.Id == id, cancellationToken);
        }

        public async Task<List<PaymentPlan>> GetByFinancialManagerId(int financialManagerId, CancellationToken cancellationToken)
        {
            return await _context.PaymentPlans
                .Include(pp => pp.Billings)
                .Where(pp => pp.FinancialManagerId == financialManagerId)
                .ToListAsync(cancellationToken);
        }

        public async Task Insert(PaymentPlan entity, CancellationToken cancellationToken)
        {
            await _context.PaymentPlans.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(PaymentPlan entity, CancellationToken cancellationToken)
        {
            _context.PaymentPlans.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}