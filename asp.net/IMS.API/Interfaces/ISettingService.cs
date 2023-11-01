using IMS.Api.Dtos.Settings;
using IMS.Contract.Common.UnitOfWorks;

namespace IMS.Api.Interfaces;
public interface ISettingService : IGenericRepository<Setting>
{
    public SettingResponse Settings(SettingRequest request);
}
