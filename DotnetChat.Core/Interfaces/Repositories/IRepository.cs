using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotnetChat.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> Get(Guid id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(Guid id);
    }
}
