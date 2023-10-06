﻿using AutoMapper;
using IMS.BusinessService.Common.UnitOfWorks;
using IMS.Contract.Common.Paging;
using IMS.Infrastructure.EnityFrameworkCore;

namespace IMS.BusinessService.Service;

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
