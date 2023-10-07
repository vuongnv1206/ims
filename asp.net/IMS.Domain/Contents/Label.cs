using IMS.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class Label : Auditable
    {
        public string Name { get; set; }
        public int IssueId { get; set; }
        [ForeignKey(nameof(IssueId))]
        public Issues Issues { get; set; }
    }
}
