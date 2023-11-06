using IMS.Api.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Models.Dtos.Projects
{
    public class ProjectRequest : PagingRequestBase
    {
        public int? ClassId { get; set; }
    }
}
