using IMS.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class Label : Auditable
    {
        public string Name { get; set; }
        public int IssueId { get; set; }
        public Issues Issues { get; set; }
    }
}
