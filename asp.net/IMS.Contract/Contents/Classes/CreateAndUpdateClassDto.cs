using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Classes
{
    public class CreateAndUpdateClassDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int SubjectId { get; set; }
        public int SettingId { get; set; }
        public Guid AssigneId { get; set; }
    }
}
