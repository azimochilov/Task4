using System.Linq.Expressions;
using Task4.Data.IRepositories;
using Task4.Domain.Commons;

namespace Task4.Data.Repositories;
public class Repository<T> : IRepository<T> where T : Auditable
{
    public ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public ValueTask<T> InsertAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public ValueTask SaveAsync()
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null, string[] includes = null)
    {
        throw new NotImplementedException();
    }

    public ValueTask<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null)
    {
        throw new NotImplementedException();
    }

    public T Update(T entity)
    {
        throw new NotImplementedException();
    }
}
