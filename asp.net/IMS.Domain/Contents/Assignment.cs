using IMS.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class Assignment : Auditable
    {
        public string Name{ get; set; }
        public string Description{ get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
