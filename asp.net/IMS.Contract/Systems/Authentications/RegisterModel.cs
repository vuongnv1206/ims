﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Contract.Systems.Authentications
{
	public class RegisterModel
	{
		public string Username { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }
	}
}
