using AirportDistanceCalculator.Core;
using AirportDistanceCalculator.External.AirportService;
using AirportDistanceCalculator.Tests.Tools;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AirportDistanceCalculator.Tests
{
	[TestClass]
	public class AirportClient_Tests
	{
		[TestMethod]
		public void Serialize_AirportDto_Test()
		{
			var dto = new AirportDtoTest { Name = "Test name" };
			var jsonString = JsonSerializer.Serialize(dto);

			Assert.AreEqual("{\"Name\":\"Test name\"}", jsonString);
		}

		[TestMethod]
		public void Deserialize_AirportDto_Test()
		{
			var jsonString = @"{""Name"":""Test name""}";

			var options = new JsonSerializerOptions
			{
			};
			var dto = JsonSerializer.Deserialize<AirportDtoTest>(jsonString, options);

			Assert.IsNotNull(dto);
			Assert.AreEqual("Test name", dto.Name);
		}

		[TestMethod]
		public void Serialize_AirportAndLocationDto_Test()
		{
			var dto = new AirportAndLocationDtoTest { Name = "Test name", Location = new LocationDtoTest { Latitude=1.1, Longitude = 2.2 } };
			var jsonString = JsonSerializer.Serialize(dto);

			Assert.AreEqual("{\"Name\":\"Test name\",\"Location\":{\"Longitude\":2.2,\"Latitude\":1.1}}", jsonString);
		}


		[TestMethod]
		public void Repo_Get_Test()
		{
			var mockFactory = new Mock<IHttpClientFactory>();

			var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
			mockHttpMessageHandler.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = HttpStatusCode.OK,
					Content = new StringContent(
						@"{
	""country"": ""Netherlands"",
	""city_iata"": ""AMS"",
	""iata"": ""AMS"",
	""city"": ""Amsterdam"",
	""timezone_region_name"": ""Europe/Amsterdam"",
	""country_iata"": ""NL"",
	""rating"": 3,
	""name"": ""Amsterdam"",
	""location"": {
		""lon"": 4.763385,
		""lat"": 52.309069
	},
	""type"": ""airport"",
	""hubs"": 7
}"),
				});

			var client = new HttpClient(mockHttpMessageHandler.Object);
			mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

			var someOptions = Options.Create(new AppSettings() { AirportServiceUrl = TestContext.AirportServiceUrl });
			var airportClient = new AirportClient(mockFactory.Object, someOptions);
			var airport = airportClient.Get("AMS").Result;

			Assert.IsNotNull(airport);
			Assert.AreEqual("AMS", airport.Iata);
			Assert.IsNotNull(airport.Location);
			Assert.AreEqual(4.763385, airport.Location.Longitude);
			Assert.AreEqual(52.309069, airport.Location.Latitude);
		}

		[TestMethod]
		public void Repo_Get_Error_Test()
		{
			var someOptions = Options.Create(new AppSettings() { AirportServiceUrl = TestContext.AirportServiceUrl });
			var airportClient = new AirportClientTest(someOptions);
			var airport = airportClient.Get("XXX").Result;

			Assert.IsNull(airport);
		}
	}

	class AirportDtoTest
	{
		public string Name { get; set; }
	}

	class AirportAndLocationDtoTest
	{
		public string Name { get; set; }
		public LocationDtoTest Location { get; set; }
	}

	public struct LocationDtoTest
	{
		public double Longitude { get; set; }
		public double Latitude { get; set; }
	}

}
