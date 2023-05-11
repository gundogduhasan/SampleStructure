using DataAccess.Repository;

namespace Business.EntityServices
{
    public interface IServiceManager<T> : IServiceManager<T, Guid> where T : AuditableEntity<Guid>
    { }

    public interface IServiceManager<T, TKey> : IRepository<T, TKey> where T : AuditableEntity<TKey>
    { }
}
