using System.Text.Json.Serialization;

namespace WeatherApp.Services
{
	public class StationData
	{
		[CsvHeader("POSTE")]
		[JsonPropertyName("post")]
		public string Post { get; set; }

		[CsvHeader("DATE")]
		[JsonPropertyName("date")]
		public DateTime Date { get; set; }

		[CsvHeader("RR1")]
		[JsonPropertyName("rainInMm")]
		public decimal RainInMm { get; set; }

		[CsvHeader("T")]
		[JsonPropertyName("temperature")]
		public decimal Temperature { get; set; }

		[CsvHeader("PMER")]
		[JsonPropertyName("pressure")]
		public decimal Pressure { get; set; }

		///// <summary>
		///// VITESSE DU VENT HORAIRE	M/S ET 1/10
		///// </summary>
		//[CsvHeader("FF")]
		//[JsonPropertyName("wind")]
		//public decimal Wind { get; set; }

		///// <summary>
		///// DIRECTION DU VENT A 10 M HORAIRE	ROSE DE 360
		///// </summary>
		//[CsvHeader("DD")]
		//[JsonPropertyName("windDirection")]
		//public decimal WindDirection { get; set; }

		/*****************/


		//[CsvHeader("U")]
		//[JsonPropertyName("humidity")]
		//public decimal Humidity { get; set; }

		///// <summary>
		///// TEMPERATURE MINIMALE SOUS ABRI HORAIRE
		///// </summary>
		//[CsvHeader("TN")]
		//[JsonPropertyName("TN")]
		//public decimal MinTemperature { get; set; }

		///// <summary>
		///// HEURE DU TN SOUS ABRI HORAIRE
		///// </summary>
		//[CsvHeader("HTN")]
		//[JsonPropertyName("HTN")]
		//public int MinTemperatureHour { get; set; }

		///// <summary>
		///// TEMPERATURE MAXIMALE SOUS ABRI HORAIRE
		///// </summary>
		//[CsvHeader("TX")]
		//[JsonPropertyName("TX")]
		//public double MaxTemperature { get; set; }

		///// <summary>
		///// HEURE DU TX SOUS ABRI HORAIRE
		///// </summary>
		//[CsvHeader("HTX")]
		//[JsonPropertyName("HTX")]
		//public int MaxTemperatureHour { get; set; }

		///// <summary>
		///// DUREE DE GEL HORAIRE
		///// </summary>
		//[CsvHeader("DG")]
		//[JsonPropertyName("DG")]
		//public int FreezeDuration { get; set; }
	}
}
