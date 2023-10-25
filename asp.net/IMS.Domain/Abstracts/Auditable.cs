using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Abstracts
{
	public class Auditable
	{
		public int Id { get; set; }
		public DateTime? CreationTime { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime? LastModificationTime { get; set; }
		public string? LastModifiedBy { get; set; }

		public Boolean IsActive { get; set; }
	}
}
