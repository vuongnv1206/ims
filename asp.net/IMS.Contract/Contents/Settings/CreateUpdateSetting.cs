using IMS.Domain.Enums;

namespace IMS.Contract.Contents.Settings;

public class CreateUpdateSetting
{
    public SettingType Type { get; set; }
    public string? Description { get; set; }
    public string Name { get; set; }
}
