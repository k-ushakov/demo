using Microsoft.AspNetCore.Mvc;

namespace AirportDistanceCalculator.WebApi.Controllers
{
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : ControllerBase
	{
		[Route("/error")]
		public IActionResult Error() => Problem();
	}
}
