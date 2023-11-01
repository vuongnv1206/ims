using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Dto.Assignments;
using IMS.Api.Dtos.Assignments;
using IMS.Api.Models.Entities;


namespace IMS.Api.Interfaces
{
    public interface IAssignmentService : IGenericRepository<Assignment>
    {

        Task<AssignmentResponse> GetAssignment(AssignmentRequest request);
    }
}
