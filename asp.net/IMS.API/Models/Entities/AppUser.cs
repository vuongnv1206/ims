using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace IMS.Api.Models.Entities
{
    public class AppUser : IdentityUser<Guid>
	{
		public string? FullName { set; get; }
		public string? Address { set; get; }
		public string? Avatar { get; set; }
		public DateTime? BirthDay { set; get; }
		public DateTime? CreationTime { get; set; }
        public ICollection<SubjectUser> SubjectUsers { get; set; }
        [JsonIgnore]
        public ICollection<Issue> Issues { get; set; }
        public ICollection<ClassStudent> ClassStudents { get; set; }
        public ICollection<ProjectMember> ProjectMembers { get; set; }
		public ICollection<Class> Class { get; set; }
        public ICollection<Subject> Subjects { get; set; }

    }
}
