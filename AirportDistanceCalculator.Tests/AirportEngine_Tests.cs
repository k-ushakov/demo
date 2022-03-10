using AirportDistanceCalculator.Application;
using AirportDistanceCalculator.Core;
using AirportDistanceCalculator.Tests.Tools;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace AirportDistanceCalculator.Tests
{
	[TestClass]
	public class AirportEngine_Tests
	{
		[TestMethod]
		public void CalculateDistance_AMS_DME_Test()
		{
			var someOptions = Options.Create(new AppSettings() { AirportServiceUrl = TestContext.AirportServiceUrl });
			var airportClient = new AirportClientTest(someOptions);
			var airportEngine = new AirportEngine(airportClient);

			int miles = airportEngine.CalculateDistance("AMS", "DME").Result;

			Assert.AreEqual(1353, miles);
		}

		[TestMethod]
		public async Task CalculateDistance_FirstAirportNotfound_Test()
		{
			var someOptions = Options.Create(new AppSettings() { AirportServiceUrl = TestContext.AirportServiceUrl });
			var airportClient = new AirportClientTest(someOptions);
			var airportEngine = new AirportEngine(airportClient);

			await Assert.ThrowsExceptionAsync<ApplicationException>(async () => {
				await airportEngine.CalculateDistance("XXX", "DME");
			}, $"Could find airport 'XXX' location.");
		}

		[TestMethod]
		public async Task CalculateDistance_SecondAirportNotfound_Test()
		{
			var someOptions = Options.Create(new AppSettings() { AirportServiceUrl = TestContext.AirportServiceUrl });
			var airportClient = new AirportClientTest(someOptions);
			var airportEngine = new AirportEngine(airportClient);

			await Assert.ThrowsExceptionAsync<ApplicationException>(async () => {
				await airportEngine.CalculateDistance("AMS", "ZZZ");
			}, $"Could find airport 'ZZZ' location.");
		}

	}

}
