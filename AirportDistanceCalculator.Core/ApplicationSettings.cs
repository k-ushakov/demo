namespace AirportDistanceCalculator.Core
{
	public class AppSettings
	{
		public const string SectionName = "AppSettings";

		public string AirportServiceUrl { get; set; }

		public string GetAirportServiceUrl(string iata)
		{
			return string.Format(AirportServiceUrl, iata);
		}
	}
}
