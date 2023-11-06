using IMS.Api.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Models.Dtos.Milestones
{
    public class MilestoneResponse : PagingResponsse
    {
        public List<MilestoneDto> Milestones { get; set; }
    }
}
