using Kedu.Domain.Entities;
using Kedu.Domain.Repositories;
using Kedu.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Kedu.Infra.Data.EF.Repositories.v1
{
    public class CostCenterRepository : ICostCenterRepository
    {
        private readonly KeduDbContext _context;

        public CostCenterRepository(KeduDbContext context)
        {
            _context = context;
        }

        public async Task Delete(CostCenter entity, CancellationToken cancellationToken)
        {
            _context.CostCenters.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<CostCenter> Get(int id, CancellationToken cancellationToken)
        {
            return await _context.CostCenters
                .FirstOrDefaultAsync(cc => cc.Id == id, cancellationToken);
        }

        public async Task<List<CostCenter>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.CostCenters
                .ToListAsync(cancellationToken);
        }

        public async Task Insert(CostCenter entity, CancellationToken cancellationToken)
        {
            await _context.CostCenters.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(CostCenter entity, CancellationToken cancellationToken)
        {
            _context.CostCenters.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}