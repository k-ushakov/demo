using AirportDistanceCalculator.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;

namespace AirportDistanceCalculator.Integration.Tests
{
	[TestClass]
	public class AirportController_Tests
	{
		private static WebApplicationFactory<Startup> _factory;

		[ClassInitialize]
		public static void ClassInit(TestContext testContext)
		{
			_factory = new WebApplicationFactory<Startup>();
		}

		[TestMethod]
		public async Task Calculate_Distance_AMS_DME()
		{
			var client = _factory.CreateClient();
			var response = await client.GetAsync("api/airport/distance/AMS/DME");

			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

			var json = await response.Content.ReadAsStringAsync();
			Assert.AreEqual("1353", json);
		}

		[TestMethod]
		public async Task Iata_Should_Be_Only_Alfa_Url_Parameter()
		{
			var client = _factory.CreateClient();
			var response = await client.GetAsync("api/airport/distance/123/DME");

			Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
		}

		[TestMethod]
		public async Task Iata_Should_Be_Three_Alfa_Url_Parameter()
		{
			var client = _factory.CreateClient();
			var response = await client.GetAsync("api/airport/distance/XXXX/DME");

			Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
		}

	}
}
