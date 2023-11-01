using IMS.Contract.Common.Paging;

namespace IMS.Api.Dtos.Settings;

public class SettingRequest : PagingRequestBase
{
    public SettingType? Type { get; set; }
}
