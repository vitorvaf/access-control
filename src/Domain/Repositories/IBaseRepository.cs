using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AccessControl.Domain.Repositories;

public interface IBaseRepository<T>
{
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<T> DeleteAsync(T entity, CancellationToken cancellationToken);
    Task<T> FindAsync(Guid id, CancellationToken cancellationToken);
    Task<T> SelectAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, int page, int pageSize, CancellationToken cancellationToken);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, int page, int pageSize, Expression<Func<T, object>> orderBy, CancellationToken cancellationToken);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, int page, int pageSize, Expression<Func<T, object>> orderBy, bool ascending, CancellationToken cancellationToken);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, int page, int pageSize, Expression<Func<T, object>> orderBy, bool ascending, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, int page, int pageSize, Expression<Func<T, object>> orderBy, bool ascending, CancellationToken cancellationToken, params string[] includes);
}
