using IMS.Contract.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Contents.Classes
{
    public class ClassReponse : PagingResponsse
    {
        public IList<ClassDto> Classes { get; set; }
    }
}
