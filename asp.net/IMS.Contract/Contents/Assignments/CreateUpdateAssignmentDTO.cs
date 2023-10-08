﻿using IMS.Domain.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Assignments
{
    public class CreateUpdateAssignmentDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int SubjectId { get; set; }

    }
}
