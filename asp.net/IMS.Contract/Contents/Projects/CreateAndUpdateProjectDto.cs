using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Projects
{
    public class CreateAndUpdateProjectDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Avatarurl { get; set; }
        public int Status { get; set; }
        public int ClassId {  get; set; }

    }
}
