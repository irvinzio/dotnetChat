using DotnetChat.Core.Interfaces.Repositories;
using DotnetChat.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotnetChat.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
    where T : class
    {
        private readonly DotnetChatContext context;
        public Repository(DotnetChatContext context)
        {
            this.context = context;
        }
        public IEnumerable<T> Get()
        {
            return context.Set<T>();
        }
        public async Task<T> Add(T entity)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Delete(Guid id)
        {
            var entity = context.Set<T>().Find(id);
            if (entity == null)
            {
                return entity;
            }
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Get(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public async Task<T> Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            var query = context.Set<T>().AsQueryable();
            return await query.Where(predicate).ToListAsync();
        }
        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        => await context.Set<T>().FirstOrDefaultAsync(predicate);
    }
}