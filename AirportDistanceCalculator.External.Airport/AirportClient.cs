using AirportDistanceCalculator.Core;
using AirportDistanceCalculator.Core.Model;
using AirportDistanceCalculator.External.AirportService.Dto;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirportDistanceCalculator.External.AirportService
{
	public class AirportClient : IAirportClient
	{
		private readonly IHttpClientFactory _clientFactory;
		private readonly AppSettings _settings;

		public AirportClient(IHttpClientFactory clientFactory, IOptions<AppSettings> settings)
		{
			_clientFactory = clientFactory;
			_settings = settings?.Value;
		}

		protected virtual HttpClient CreateClient()
		{
			return _clientFactory.CreateClient();
		}

		public async Task<Airport> Get(string iata)
		{
			Airport result = null;

			var request = new HttpRequestMessage(HttpMethod.Get, _settings.GetAirportServiceUrl(iata));

			var client = CreateClient();

			var response = await client.SendAsync(request);

			if (response.IsSuccessStatusCode)
			{
				using var responseStream = await response.Content.ReadAsStreamAsync();
				var airportDto = await JsonSerializer.DeserializeAsync<AirportDto>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				// TODO: use automapper
				result = new Airport
				{
					Iata = airportDto.Iata,
					Location = new Location
					{
						Latitude = airportDto.Location.Lat,
						Longitude = airportDto.Location.Lon
					}
				};
			}

			return result;
		}
	}
}
