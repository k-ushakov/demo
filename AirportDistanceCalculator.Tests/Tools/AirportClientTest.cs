using AirportDistanceCalculator.Core;
using AirportDistanceCalculator.External.AirportService;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace AirportDistanceCalculator.Tests.Tools
{
	class AirportClientTest : AirportClient
	{
		public AirportClientTest(IOptions<AppSettings> settings) 
			: base(null, settings) { }

		protected override HttpClient CreateClient()
		{
			return new HttpClient();
		}
	}

}
