using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.APIControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ControllerBase : Controller
	{
        public ControllerBase()
        {
            
        }
    }
}
