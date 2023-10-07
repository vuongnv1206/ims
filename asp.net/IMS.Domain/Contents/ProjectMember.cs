using IMS.Domain.Abstracts;
using IMS.Domain.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class ProjectMember
    {
        public Guid UserId { get; set; }
        public int ProjectId { get; set; }
        public AppUser User { get; set; }
        public Project Project { get; set; }
    }
}
