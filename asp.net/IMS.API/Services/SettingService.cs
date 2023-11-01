using AutoMapper;
using IMS.BusinessService.Service;
using IMS.Contract.Common.Sorting;
using IMS.Contract.Contents.Settings;
using IMS.Domain.Contents;
using IMS.Api.EnityFrameworkCore;

namespace IMS.Api.Services;

public class SettingService : ServiceBase<Setting>, ISettingService
{
    public SettingService(
        IMSDbContext context, 
        IMapper mapper) 
        : base(context, mapper)
    {
    }

    public SettingResponse Settings(SettingRequest request)
    {
        var settings = context.Settings
            .Where(x => string.IsNullOrEmpty(request.KeyWords)
            || x.Name.Contains(request.KeyWords)
            || x.Description.Contains(request.KeyWords)).ToList();

        if (request.Type != null)
        {
            settings = settings.Where(x => x.Type == request.Type).ToList();
        }

        var settingMap = mapper.Map<List<SettingDto>>(settings).Paginate(request).ToList();

        return new SettingResponse
        {
            Settings = settingMap,
            Page = GetPagingResponse(request, settings.Count)
        };

    }

}
