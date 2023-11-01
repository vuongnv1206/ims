using AutoMapper;
using IMS.BusinessService.Service;
using IMS.Contract.Common.Sorting;
using IMS.Contract.Contents.Assignments;
using IMS.Contract.Systems.Users;
using IMS.Domain.Contents;
using IMS.Api.EnityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Services
{
    public class AssignmentService : ServiceBase<Assignment>, IAssignmentService
    {
        
        public AssignmentService(IMSDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<AssignmentResponse> GetAssignment(AssignmentRequest request)
        {
            var assignmentQuery = await context.Assignments
                .Where(u => string.IsNullOrWhiteSpace(request.KeyWords)
                        || u.Name.Contains(request.KeyWords)
                        || u.Description.Contains(request.KeyWords)).ToListAsync();

            var assignments = assignmentQuery.Paginate(request);
            if(request.SubjectId != null) 
            { 
                assignments = assignments.Where(x => x.SubjectId == request.SubjectId);
            }
            var assignmentDtos = mapper.Map<List<AssignmentDTO>>(assignments);

            

            var response = new AssignmentResponse
            {
                Assignments = assignmentDtos,
                Page = GetPagingResponse(request, assignmentQuery.Count()),
            };

            return response;



        }

        public Task<CreateUpdateAssignmentDTO> GetAssignmentBySubjectId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
