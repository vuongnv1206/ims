using AutoMapper;
using IMS.Api.EnityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IMS.Api.Models.Entities;
using IMS.Api.Interfaces;
using IMS.Api.Models.Dtos.Assignments;
using IMS.Api.Common.Helpers.Extensions;

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

            if(request.SubjectId != null) 
            { 
                assignmentQuery = assignmentQuery.Where(x => x.SubjectId == request.SubjectId).ToList();
            }
            var assignmentDtos = mapper.Map<List<AssignmentDTO>>(assignmentQuery).ToList();

            

            var response = new AssignmentResponse
            {
                Assignments = assignmentDtos,
                Page = GetPagingResponse(request, assignmentQuery.Count()),
            };

            return response;



        }
    }
}
