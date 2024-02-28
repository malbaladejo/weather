using System.Diagnostics;
using System.Globalization;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    internal class InFactoryFileReader : IWeatherFileReader
    {
        private readonly ILogger<CsvWeatherService> logger;
        private static readonly CultureInfo dateCultureInfo = new CultureInfo("fr-FR");
        private static readonly CultureInfo decimalCultureInfo = new CultureInfo("en-US");
        private const string csvSeparator = "\t";

        public InFactoryFileReader(ILogger<CsvWeatherService> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<WeatherData> Parse(string inputFile)
        {
            if (!File.Exists(inputFile))
            {
                this.logger.LogInformation($"The file {inputFile} does not exist.");
                yield break;
            }

            this.logger.LogInformation($"Load file {inputFile}.");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var sr = new StreamReader(inputFile))
            {
                var line = string.Empty;
                Dictionary<string, int> headers = null;
                while ((line = sr.ReadLine()) != null)
                {
                    if (headers == null)
                    {
                        headers = GetCsvHeader(line);
                        continue;
                    }

                    var dayData = ConvertLineToDto(line, headers);
                    if (dayData != null)
                        yield return dayData;
                }
            }
            stopwatch.Stop();
            this.logger.LogInformation($"File {inputFile} loaded in {stopwatch.ElapsedMilliseconds}ms.");
        }

        private void CheckFile(string[] headerData)
        {
            var missingColumns = CsvColumns.ColumnsToRead.Where(c1 => !headerData.Contains(c1));

            foreach (var columnName in missingColumns)
            {
                this.logger.LogWarning($"column {columnName} not found.");
            }
        }

        private Dictionary<string, int> GetCsvHeader(string csvHeaderLine)
        {
            var headerData = csvHeaderLine.Split(csvSeparator);
            this.CheckFile(headerData);

            var headers = new Dictionary<string, int>();

            for (int i = 0; i < headerData.Length; i++)
                headers[headerData[i]] = i;

            return headers;
        }

        private WeatherData ConvertLineToDto(string line, Dictionary<string, int> headers)
        {
            var lineData = line.Split(csvSeparator);

            try
            {
                return new WeatherData
                {
                    Date = ParseDate(GetData(lineData, CsvColumns.Time, headers)),
                    OutHumidity = ParseInt(GetData(lineData, CsvColumns.Humi, headers)),
                    InHumidity = ParseInt(GetData(lineData, CsvColumns.InHumi, headers)),
                    OutTemperature = ParseDecimal(GetData(lineData, CsvColumns.Temp, headers)),
                    InTemperature = ParseDecimal(GetData(lineData, CsvColumns.InTemp, headers)),
                    Rain = ParseDecimal(GetData(lineData, CsvColumns.RainHour, headers)),
                    Wind = ParseDecimal(GetData(lineData, CsvColumns.Wind, headers)),
                    WindDirection = GetData(lineData, CsvColumns.WindDirection, headers),
                    AbsolutePressure = ParseDecimal(GetData(lineData, CsvColumns.ABSPressure, headers)),
                    RelativePressure = ParseDecimal(GetData(lineData, CsvColumns.RELPressure, headers))
                };
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"{string.Join(',', lineData)}");
                return null;
            }
        }

        private string GetData(string[] lineData, string columnName, Dictionary<string, int> headers)
        {
            if (!headers.ContainsKey(columnName))
            {
                return null;
            }

            var columnIndex = headers[columnName];

            return lineData[columnIndex];
        }

        private static DateTime ParseDate(string value)
        {
            return DateTime.Parse(value, dateCultureInfo);
        }

        private static int? ParseInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (int.TryParse(value, out int valueInt))
            {
                return valueInt;
            }

            return null;
        }

        private static decimal? ParseDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (decimal.TryParse(value, NumberStyles.Any, decimalCultureInfo, out decimal valueInt))
            {
                return valueInt;
            }

            return null;
        }
    }
}
