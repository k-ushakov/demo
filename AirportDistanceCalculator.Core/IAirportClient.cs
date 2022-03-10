using AirportDistanceCalculator.Core.Model;
using System.Threading.Tasks;

namespace AirportDistanceCalculator.Core
{
	public interface IAirportClient
	{
		Task<Airport> Get(string iata);
	}
}