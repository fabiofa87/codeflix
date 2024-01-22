namespace CodeflixCatalogDomain.SeedWork;

public interface IGenericRepository<TAggregate> : IRepository
{
    public Task Insert(TAggregate aggregate, CancellationToken cancellationToken);
    public Task<TAggregate> GetById(Guid id, CancellationToken cancellationToken);
    
}