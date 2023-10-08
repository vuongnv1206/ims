using IMS.Contract.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Assignments
{
    public class AssignmentResponse : PagingResponsse
    {
        public List<AssignmentDTO> Assignments { get; set; }

    }
}
