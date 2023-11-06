using IMS.Api.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Models.Dtos.Subjects
{
    public class SubjectReponse : PagingResponsse
    {
        public IList<SubjectDto> Subjects { get; set; }

    }
}
