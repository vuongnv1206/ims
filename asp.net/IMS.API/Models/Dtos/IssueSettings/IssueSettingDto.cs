using IMS.Api.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Api.Models.Dtos.IssueSettings
{
    public class IssueSettingDto
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }

        public int? ClassId { get; set; }

        public int? SubjectId { get; set; }
        
        public  Project? Project { get; set; }
        
        public  Subject? Subject { get; set; }
       
        public  Class? Class { get; set; }
            
    }
}
