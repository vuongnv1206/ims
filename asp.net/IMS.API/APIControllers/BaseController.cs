using Microsoft.AspNetCore.Mvc;

namespace IMS.Api.APIControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BaseController : Controller
	{
        public BaseController()
        {
            
        }
    }
}
