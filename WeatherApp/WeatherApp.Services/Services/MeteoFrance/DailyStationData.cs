namespace WeatherApp.Services
{
    public class DailyStationData : StationData
    {
        [CsvHeader("POSTE")]
        public string Post { get; set; }

        [CsvHeader("DATE")]
        public DateTime Date { get; set; }

        [CsvHeader("RR")]
        public decimal RainInMm { get; set; }

        /// <summary>
		///  DRR DUREE DES PRECIPITATIONS QUOTIDIENNES
		/// </summary>
        [CsvHeader("DRR")]
        public decimal RainDurationInMin { get; set; }

        [CsvHeader("TN")]
        public decimal MinTemperature { get; set; }

        [CsvHeader("TX")]
        public decimal MaxTemperature { get; set; }

        [CsvHeader("DG")]
        public decimal FreezeDurationInMinutes { get; set; }


        [CsvHeader("PMER")]
        public decimal Pressure { get; set; }
    }
}
