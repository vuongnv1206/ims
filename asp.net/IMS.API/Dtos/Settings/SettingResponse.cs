using IMS.Api.Common.Paging;

namespace IMS.Api.Dtos.Settings;

public class SettingResponse : PagingResponsse
{
    public List<SettingDto> Settings { get; set; }
}
