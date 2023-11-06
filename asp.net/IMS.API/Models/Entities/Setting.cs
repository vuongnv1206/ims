using IMS.Api.Models.Abstracts;
using IMS.Api.Models.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IMS.Api.Models.Entities
{
    public class Setting : Auditable
    {
        public SettingType Type { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Class>? Classes { get; set; }

    }
}
