using IMS.Contract.Common.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BusinessService.Common.UnitOfWorks
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IMSDbContext context;

        public GenericRepository(IMSDbContext context)
        {
            this.context = context;
        }


        public async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public async Task DeleteManyAsync(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = context.Set<T>().Where(predicate).AsEnumerable();
            context.RemoveRange(entities);
        }



        public async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).CountAsync();
        }

        public IQueryable<T> GetListAsync()
        {
            return context.Set<T>().AsQueryable();
        }

        public async Task<T> InsertAsync(T entity)
        {
            context.Set<T>().Add(entity);
            return entity;
        }

        public async Task InsertManyAsync(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }

        public IQueryable<T> GetListAsyncWithDetails(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] propertySelectors)
        {
            predicate = predicate ?? (x => true);

            var query = context.Set<T>().Where(predicate).AsQueryable();

            if (propertySelectors != null && propertySelectors.Length > 0)
            {
                foreach (var selector in propertySelectors)
                {
                    query = query.Include(selector);
                }
            }

            return query;
        }


        public async Task<T> UpdateAsync(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task UpdateManyAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                context.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task<T> GetWithDetails(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] propertySelectors)
        {
            predicate = predicate ?? (x => true);

            if (propertySelectors != null && propertySelectors.Length > 0)
            {
                var query = context.Set<T>().Include(propertySelectors.First());
                foreach (var property in propertySelectors.Skip(1))
                    query = query.Include(property);
                return await query.FirstOrDefaultAsync(predicate);
            }

            return await context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {

            var entity = await GetById(id);
            return entity != null;
        }
    }

}
