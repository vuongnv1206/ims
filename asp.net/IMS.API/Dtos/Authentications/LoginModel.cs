﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Api.Dtos.Authentications
{
	public class LoginModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
