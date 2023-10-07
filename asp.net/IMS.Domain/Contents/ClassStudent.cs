using IMS.Domain.Abstracts;
using IMS.Domain.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class ClassStudent
    {
        public Guid UserId { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public AppUser User{ get; set; }
    }
}
