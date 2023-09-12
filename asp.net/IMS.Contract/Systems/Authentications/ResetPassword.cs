﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Systems.Authentications
{
	public class ResetPassword
	{
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }

	}
}
