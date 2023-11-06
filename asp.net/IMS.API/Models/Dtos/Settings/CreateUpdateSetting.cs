using IMS.Api.Models.Enums;

namespace IMS.Api.Models.Dtos.Settings
{
    public class CreateUpdateSetting
    {
        public SettingType Type { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }
    }
}