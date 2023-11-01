using IMS.Contract.Common.Paging;
using IMS.Contract.Contents.Milestones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Issues
{
    public class IssueResponse : PagingResponsse
    {
        public List<IssueDto> Issues { get; set; }
    }
}
