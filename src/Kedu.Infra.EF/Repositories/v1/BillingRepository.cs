using Kedu.Domain.Entities;
using Kedu.Domain.Repositories;
using Kedu.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Kedu.Domain.Enums;

namespace Kedu.Infra.Data.EF.Repositories.v1
{
    public class BillingRepository : IBillingRepository
    {
        private readonly KeduDbContext _context;

        public BillingRepository(KeduDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Billing entity, CancellationToken cancellationToken)
        {
            _context.Billings.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Billing> Get(int id, CancellationToken cancellationToken)
        {
            return await _context.Billings
                .Include(b => b.PaymentPlan)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<List<Billing>> GetByFinancialManagerId(
            int financialManagerId,
            StatusBilling? status,
            PaymentMethod? paymentMethod,
            DateTime? startDate,
            DateTime? endDate,
            CancellationToken cancellationToken)
        {
            var query = _context.Billings
                .Include(b => b.PaymentPlan)
                .Where(b => b.PaymentPlan.FinancialManagerId == financialManagerId);

            if (status.HasValue)
            {
                query = query.Where(b => b.StatusBilling == status.Value);
            }

            if (paymentMethod.HasValue)
            {
                query = query.Where(b => b.PaymentMethod == paymentMethod.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(b => b.DueDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(b => b.DueDate <= endDate.Value);
            }

            return await query
                .OrderBy(b => b.DueDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetCountByFinancialManagerId(
            int financialManagerId,
            StatusBilling? status,
            PaymentMethod? paymentMethod,
            CancellationToken cancellationToken)
        {
            var query = _context.Billings
                .Include(b => b.PaymentPlan)
                .Where(b => b.PaymentPlan.FinancialManagerId == financialManagerId);

            if (status.HasValue)
            {
                query = query.Where(b => b.StatusBilling == status.Value);
            }

            if (paymentMethod.HasValue)
            {
                query = query.Where(b => b.PaymentMethod == paymentMethod.Value);
            }

            return await query.CountAsync(cancellationToken);
        }

        public async Task Insert(Billing entity, CancellationToken cancellationToken)
        {
            await _context.Billings.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Billing entity, CancellationToken cancellationToken)
        {
            _context.Billings.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}