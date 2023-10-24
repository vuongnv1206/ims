using IMS.Contract.Common.Paging;
using IMS.Contract.Contents.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Projects
{
    public class ProjectReponse : PagingResponsse
    {
        public IList<ProjectDto> Projects{ get; set; }
    }
}
