﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Models.Dtos.Roles
{
    public class PermissionDto
    {
        public string RoleId { get; set; }
        public IList<RoleClaimDto> RoleClaims { get; set; }
    }
}
