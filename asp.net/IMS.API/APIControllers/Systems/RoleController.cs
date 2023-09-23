using IMS.Contract.Common.Responses;
using IMS.Contract.Systems.Authentications;
using IMS.Contract.Systems.Roles;
using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.APIControllers.Systems
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleSerive;
        public RoleController(IRoleService roleSerive)
        {
            _roleSerive = roleSerive;
        }
		//alo
        [HttpGet("roles")]
		public async Task<IActionResult> GetAllRoles ()
		{
			return Ok(await _roleSerive.GetListAllAsync());  
		}
	}
}
