using System.Threading.Tasks;

namespace AirportDistanceCalculator.Core
{
	public interface IAirportEngine
	{
		Task<int> CalculateDistance(string iata1, string iata2);
	}
}