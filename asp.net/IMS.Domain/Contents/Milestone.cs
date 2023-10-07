using IMS.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class Milestone : Auditable
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public ICollection<Issues> Issues { get; set; } 

    }
}
