using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Models.Dtos.Settings;
using IMS.Api.Models.Entities;

namespace IMS.Api.Interfaces;
public interface ISettingService : IGenericRepository<Setting>
{
    public SettingResponse Settings(SettingRequest request);
}
