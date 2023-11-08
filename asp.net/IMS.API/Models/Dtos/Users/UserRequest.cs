using IMS.Api.Common.Paging;

namespace IMS.Api.Models.Dtos.Users;

public class UserRequest : PagingRequestBase
{
    public int? ClassId { get; set; }
    public int? ProjectId { get; set; }
}
