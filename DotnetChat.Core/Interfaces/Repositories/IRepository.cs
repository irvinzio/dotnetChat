using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DotnetChat.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        T Get(Guid id);
        T Add(T entity);
        T Update(T entity);
        T Delete(Guid id);
    }
}
