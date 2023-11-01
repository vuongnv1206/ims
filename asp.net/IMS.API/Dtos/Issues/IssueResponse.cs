using IMS.Contract.Common.Paging;
using IMS.Contract.Contents.Milestones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Dtos.Issues
{
    public class IssueResponse : PagingResponsse
    {
        public List<IssueDto> Issues { get; set; }
    }
}
