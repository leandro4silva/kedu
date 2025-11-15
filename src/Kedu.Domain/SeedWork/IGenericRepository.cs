namespace Kedu.Domain.SeedWork
{
    public interface IGenericRepository<TEntity> : IRepository
        where TEntity : class
    {
        public Task Insert(TEntity entity, CancellationToken cancellationToken);
        public Task<TEntity> Get(int id, CancellationToken cancellationToken);
        public Task Delete(TEntity entity, CancellationToken cancellationToken);
        public Task Update(TEntity entity, CancellationToken cancellationToken);
    }
}
