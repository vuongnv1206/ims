using IMS.Api.Common.Paging;
using IMS.Api.Models.Dtos.Issues;

namespace IMS.Api.Models.Dtos.IssueSettings
{
    public class IssueSettingResponse : PagingResponsse
    {      
            public List<IssueSettingDto> IssueSettings { get; set; }
        
    }
}
