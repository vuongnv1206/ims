using IMS.Contract.Common.UnitOfWorks;
using IMS.Infrastructure.EnityFrameworkCore;
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

		public async Task<bool> ExistsAsync(Guid id)
		{
			var entity = await GetById(id);
			return entity != null;
		}

		public async Task<T> GetById(Guid id)
		{
			return await context.Set<T>().FindAsync(id);
		}


		public async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate)
		{
			return await context.Set<T>().Where(predicate).CountAsync();
		}

		public IQueryable<T> GetListAsync()
		{
			return  context.Set<T>().AsQueryable();
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
			var query = context.Set<T>().Where(predicate).AsQueryable();
			foreach (var selector in propertySelectors)
			{
				query = query.Include(selector);
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
			//await _context.SaveChangesAsync();
		}

		public async Task<T> GetWithDetails(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] propertySelectors)
		{
			//var entity = await GetById(id);

			//foreach (var selector in propertySelectors)
			//{
			//	entity = _context.Set<T>().Include(selector).FirstOrDefault();
			//}

			//return entity;
			if (propertySelectors != null && propertySelectors.Count() > 0)
			{
				var query = context.Set<T>().Include(propertySelectors.First());
				foreach (var property in propertySelectors.Skip(1))
					query = query.Include(property);
				return query.FirstOrDefault(predicate);
			}
			return context.Set<T>().FirstOrDefault(predicate);
		}

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
