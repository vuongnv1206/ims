﻿using IMS.Contract.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Assignments
{
    public class AssignmentRequest : PagingRequestBase
    {
        public int? SubjectId { get; set; }
    }
}
