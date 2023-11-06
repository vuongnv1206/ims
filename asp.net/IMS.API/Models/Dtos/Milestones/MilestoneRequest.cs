using IMS.Api.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Models.Dtos.Milestones
{
    public class MilestoneRequest : PagingRequestBase
    {
        public int? ProjectId { get; set; }
        public int? ClassId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
