using IMS.Contract.Common.Paging;
using IMS.Contract.Contents.Assignments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Milestones
{
    public class MilestoneResponse : PagingResponsse
    {
        public List<MilestoneDto> Milestones { get; set; }
    }
}
