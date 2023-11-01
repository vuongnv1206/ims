using IMS.Api.Common.Paging;
using IMS.Api.Models.Enums;

namespace IMS.Api.Dtos.Settings;

public class SettingRequest : PagingRequestBase
{
    public SettingType? Type { get; set; }
}
