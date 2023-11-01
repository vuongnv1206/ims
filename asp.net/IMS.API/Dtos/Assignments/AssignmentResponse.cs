using IMS.Contract.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Dtos.Assignments
{
    public class AssignmentResponse : PagingResponsse
    {
        public List<AssignmentDTO> Assignments { get; set; }

    }
}
