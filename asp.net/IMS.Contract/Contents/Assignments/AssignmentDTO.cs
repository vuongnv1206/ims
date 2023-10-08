﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Assignments
{
    public class AssignmentDTO
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int SubjectId { get; set; }
    }
}
