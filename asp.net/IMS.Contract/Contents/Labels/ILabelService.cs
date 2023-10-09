using IMS.Contract.Common.UnitOfWorks;
using IMS.Domain.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Labels
{
    public interface ILabelService : IGenericRepository<Label>
    {

        Task<LabelResponse> GetLabel(LabelRequest request);
    }
}
