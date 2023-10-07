using IMS.Domain.Abstracts;
using IMS.Domain.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class SubjectUser
    {
        public int UserId { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public AppUser User { get; set; }

    }
}
