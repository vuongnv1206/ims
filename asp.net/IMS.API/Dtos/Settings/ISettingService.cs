using IMS.Contract.Common.UnitOfWorks;

namespace IMS.Api.Dtos.Settings;
public interface ISettingService : IGenericRepository<Setting>
{
    public SettingResponse Settings(SettingRequest request);
}
