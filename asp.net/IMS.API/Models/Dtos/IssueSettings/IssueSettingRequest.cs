using IMS.Api.Common.Paging;
using IMS.Api.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Api.Models.Dtos.IssueSettings
{
    public class IssueSettingRequest : PagingRequestBase
    {
        public int? ProjectId { get; set; }

        public int? ClassId { get; set; }

        public int? SubjectId { get; set; }


    }
}
