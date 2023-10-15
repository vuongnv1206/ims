using IMS.Contract.Common.Paging;

namespace IMS.Contract.Contents.Settings;

public class SettingResponse : PagingResponsse
{
    public List<SettingDto> Settings { get; set; }
}
