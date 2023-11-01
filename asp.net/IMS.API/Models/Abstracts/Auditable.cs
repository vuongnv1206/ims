namespace IMS.Api.Models.Abstracts
{
    public class Auditable
	{
		public int Id { get; set; }
		public DateTime? CreationTime { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime? LastModificationTime { get; set; }
		public string? LastModifiedBy { get; set; }

		public bool? IsActive { get; set; }
	}
}
