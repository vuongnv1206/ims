using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Common.UnitOfWorks
{
	public interface IGenericRepository<T> where T : class
	{
		//Get a
		Task<T> GetById(Guid id);
		Task<T> GetWithDetails(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] propertySelectors);


		//Queriable
		IQueryable<T> GetListAsync();
		IQueryable<T> GetListAsyncWithDetails(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] propertySelectors);


		//Insert
		Task<T> InsertAsync(T entity);
		Task InsertManyAsync(IEnumerable<T> entities);

		//Update
		Task<T> UpdateAsync(T entity);
		Task UpdateManyAsync(IEnumerable<T> entities);


		//Delete
		Task DeleteAsync(T entity);
		Task DeleteManyAsync(Expression<Func<T, bool>> predicate);


		//Others
		Task<bool> ExistsAsync(Guid id);
		Task<int> GetCountAsync(Expression<Func<T, bool>> predicate);
	}
}
