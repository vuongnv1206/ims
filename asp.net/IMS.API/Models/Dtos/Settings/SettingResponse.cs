using IMS.Api.Common.Paging;

namespace IMS.Api.Models.Dtos.Settings;

public class SettingResponse : PagingResponsse
{
    public List<SettingDto> Settings { get; set; }
}
