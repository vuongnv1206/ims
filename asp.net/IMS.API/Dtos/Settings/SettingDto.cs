
namespace IMS.Api.Dtos.Settings;

public class SettingDto
{
    public int Id { get; set; }
    public SettingType Type { get; set; }
    public string? Description { get; set; }
    public string Name { get; set; }
}
