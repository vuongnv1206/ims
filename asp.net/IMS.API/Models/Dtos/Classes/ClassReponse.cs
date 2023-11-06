using IMS.Api.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Models.Dtos.Classes
{
    public class ClassReponse : PagingResponsse
    {
        public List<ClassDto> Classes { get; set; }
    }
}
