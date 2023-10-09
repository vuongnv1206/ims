using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Common.UnitOfWorks
{
	public interface IUnitOfWork : IDisposable
	{
		Task SaveChangesAsync();
	}
}
