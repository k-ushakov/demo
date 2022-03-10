using AirportDistanceCalculator.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AirportDistanceCalculator.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AirportController : ControllerBase
	{
		private readonly IAirportEngine _airportEngine;

		public AirportController(IAirportEngine airportEngine)
		{
			_airportEngine = airportEngine;
		}

		[HttpGet]
		[Route("distance/{airport1:alpha:length(3)}/{airport2:alpha:length(3)}")]
		[Produces("application/json")]
		public async Task<int> DistanceBetweeenAirports(string airport1, string airport2)
		{
			return await _airportEngine.CalculateDistance(airport1, airport2);
		}
	}
}
