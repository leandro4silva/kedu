using Kedu.Domain.Entities;
using Kedu.Domain.SeedWork;

namespace Kedu.Domain.Repositories
{
    public interface ICostCenterRepository :
        IGenericRepository<CostCenter>
    {
        Task<List<CostCenter>> GetAll(CancellationToken cancellationToken);
    }
}
