using AutoMapper;
using IMS.Infrastructure.EnityFrameworkCore;

namespace IMS.BusinessService.Service;

public abstract class ServiceBase
{
    protected readonly IMSDbContext context;
    protected readonly IMapper mapper;
    public ServiceBase(
        IMSDbContext context,
        IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
}
