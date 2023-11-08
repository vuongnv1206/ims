using IMS.Api.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Models.Dtos.ProjectMembers
{
    public class ProjectMemberReponse : PagingResponsse
    {
        public List<ProjectMemberDto>? ProjectMember { get; set; }
    }
}
