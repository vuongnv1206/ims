﻿using IMS.Api.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Dtos.Projects
{
    public class ProjectReponse : PagingResponsse
    {
        public IList<ProjectDto> Projects{ get; set; }
    }
}
