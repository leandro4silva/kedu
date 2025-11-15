using Kedu.Domain.Entities;
using Kedu.Domain.Repositories;
using Kedu.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Kedu.Infra.Data.EF.Repositories.v1
{
    public class FinancialManagerRepository : IFinancialManagerRepository
    {
        private readonly KeduDbContext _context;

        public FinancialManagerRepository(KeduDbContext context)
        {
            _context = context;
        }

        public async Task Delete(FinancialManager entity, CancellationToken cancellationToken)
        {
            _context.FinancialManagers.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<FinancialManager> Get(int id, CancellationToken cancellationToken)
        {
            return await _context.FinancialManagers
                .FirstOrDefaultAsync(fm => fm.Id == id, cancellationToken);
        }

        public async Task Insert(FinancialManager entity, CancellationToken cancellationToken)
        {
            await _context.FinancialManagers.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(FinancialManager entity, CancellationToken cancellationToken)
        {
            _context.FinancialManagers.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}