using IMS.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Contents
{
    public class User : Auditable
    {
        //public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public int SettingId { get; set; }
        public bool NomalizedEmail { get; set; }
        public string NomalizedUserName { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string LookoutEnable { get; set; }
        public int AccessFailCount { get; set; }
        public string Name {  get; set; }
        public Setting Setting { get; set; }
        public ICollection<SubjectUser> SubjectUsers {  get; set; }  
        public ICollection<Issues> Issues { get; set; }
        public ICollection<ClassStudent> ClassStudents { get; set; }
        public ICollection<ProjectMember> ProjectMembers { get; set; }
    }
}
