using Kedu.Domain.SeedWork;
using Kedu.Infra.Context;

namespace Kedu.Infra.Data.EF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KeduDbContext _context;

        public UnitOfWork(KeduDbContext context)
        {
            _context = context;
        }

        public async Task Commit(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task Rollback(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
