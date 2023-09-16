namespace IMS.Contract.Systems.Tokens;

public class Token
{
    public string AccessToken { get; set; }
    public DateTime Expire { get; set; }
    public UserDto User { get; set; }
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? StartWorkDate { get; set; }
        public DateTime? EndWorkDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}
