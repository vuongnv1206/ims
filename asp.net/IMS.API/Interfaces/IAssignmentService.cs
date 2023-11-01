using IMS.Api.Dtos.Assignments;
using IMS.Contract.Common.UnitOfWorks;
using IMS.Domain.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Interfaces
{
    public interface IAssignmentService : IGenericRepository<Assignment>
    {

        Task<AssignmentResponse> GetAssignment(AssignmentRequest request);
    }
}
