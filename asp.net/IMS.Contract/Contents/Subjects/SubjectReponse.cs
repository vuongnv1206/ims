using IMS.Contract.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Subjects
{
    public class SubjectReponse : PagingResponsse
    {
        public IList<SubjectDto> Subjects { get; set; }

    }
}
