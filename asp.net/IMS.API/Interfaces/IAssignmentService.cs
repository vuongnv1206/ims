using IMS.Api.Common.UnitOfWorks;
using IMS.Api.Dtos.Assignments;
using IMS.Api.Models.Entities;
using IMS.Contract.Contents.Assignments;

namespace IMS.Api.Interfaces
{
    public interface IAssignmentService : IGenericRepository<Assignment>
    {

        Task<AssignmentResponse> GetAssignment(AssignmentRequest request);
    }
}
