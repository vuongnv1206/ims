﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Common.Helpers.Settings
{
    public class JwtSetting
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
    }
}
