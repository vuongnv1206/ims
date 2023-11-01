using AutoMapper;
using IMS.Api.Common.Paging;
using IMS.Api.EnityFrameworkCore;
using IMS.Api.Services.UnitOfWorks;

namespace IMS.Api.Services;

public abstract class ServiceBase<T> : GenericRepository<T> where T : class
{
    //protected readonly IMSDbContext context;
    protected readonly IMapper mapper;
    public ServiceBase(IMSDbContext context,IMapper mapper) : base(context)
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
