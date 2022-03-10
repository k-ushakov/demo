using AirportDistanceCalculator.Core;
using System;
using System.Threading.Tasks;

namespace AirportDistanceCalculator.Application
{
	public class AirportEngine : IAirportEngine
	{
		private readonly IAirportClient _airportClient;

		public AirportEngine(IAirportClient airportClient)
		{
			_airportClient = airportClient;
		}

		public async Task<int> CalculateDistance(string iata1, string iata2)
		{
			// task - запросить инфу о аэропорте1
			var getAirport1Task = _airportClient.Get(iata1);

			// task - запросить инфу о аэропорте2
			var getAirport2Task = _airportClient.Get(iata2);

			await Task.WhenAll(getAirport1Task, getAirport2Task);

			var airport1 = getAirport1Task.Result;
			if (airport1 == null) throw AirportNotFound(iata1);

			var airport2 = getAirport2Task.Result;
			if (airport2 == null) throw AirportNotFound(iata2);

			return Calculator.DistanceBetween(airport1.Location, airport2.Location)
				.MetresToMiles();
		}

		private ApplicationException AirportNotFound(string iata)
		{
			return new ApplicationException($"Could find airport '{iata}' location.");
		}
	}
}