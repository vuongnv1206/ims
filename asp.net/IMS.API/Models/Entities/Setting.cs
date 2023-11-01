﻿using IMS.Api.Models.Abstracts;

using IMS.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Models.Entities
{
    public class Setting : Auditable
    {
        public SettingType Type { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Class>? Classes { get; set; }

    }
}
