

namespace IMS.Api.Dtos.Settings
{
    public class CreateUpdateSetting
    {
        public SettingType Type { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }
    }
}