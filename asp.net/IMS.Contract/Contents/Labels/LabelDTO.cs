using IMS.Domain.Contents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Labels
{
    public class LabelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IssueId { get; set; }
        [ForeignKey(nameof(IssueId))]
        public Issues Issues { get; set; }
    }
}
