using IMS.Contract.Common.UnitOfWorks;
using IMS.Domain.Contents;

namespace IMS.Contract.Contents.Settings;

public interface ISettingService : IGenericRepository<Setting>
{
    public SettingResponse Settings(SettingRequest request);
}
