using IMS.Contract.Common.Paging;
using IMS.Domain.Enums;

namespace IMS.Contract.Contents.Settings;

public class SettingRequest : PagingRequestBase
{
    public SettingType? Type { get; set; }
}
