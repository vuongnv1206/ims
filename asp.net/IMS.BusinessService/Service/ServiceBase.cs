using AutoMapper;
using IMS.Contract.Common.Paging;
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

    public ServiceBase(IMSDbContext context)
    {
        this.context = context;
    }

    public ServiceBase(IMapper mapper)
    {
        this.mapper = mapper;
    }

    public static PagingResponseInfo GetPagingResponse(PagingRequestBase request, int totalRecord)
    {
        return new PagingResponseInfo
        {
            CurrentPage = request.Page,
            ItemsPerPage = request.ItemsPerPage,
            ToTalPage = (totalRecord / request.ItemsPerPage) + ((totalRecord % request.ItemsPerPage) == 0 ? 0 : 1),
            ToTalRecord = totalRecord
        };
    }
}
