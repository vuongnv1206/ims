using AutoMapper;
using IMS.BusinessService.Service;
using IMS.Contract.Common.Sorting;
using IMS.Contract.Contents.Assignments;
using IMS.Contract.Contents.Labels;
using IMS.Domain.Contents;
using IMS.Infrastructure.EnityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BusinessService.Systems
{
    internal class LabelService : ServiceBase<Label>, ILabelService
    {
        public LabelService(IMSDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<LabelResponse> GetLabel(LabelRequest request)
        {
            var labelQuery = await context.Labels
                .Where(u => string.IsNullOrWhiteSpace(request.KeyWords)
                        || u.Name.Contains(request.KeyWords)).ToListAsync();

            var labels = labelQuery.Paginate(request);
            var labelDtos = mapper.Map<List<LabelDTO>>(labels);



            var response = new LabelResponse
            {
                Labels = labelDtos,
                Page = GetPagingResponse(request, labelQuery.Count()),
            };

            return response;

        }

        public Task<CreateUpdateLabelDTO> GetLabelByIssueId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
