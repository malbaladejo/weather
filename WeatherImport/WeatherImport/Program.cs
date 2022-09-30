using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace WeatherImport
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");

            if (!TryGetInputFolder(args, out var inputFolder))
                return;

            var folderManager = new FolderManager(inputFolder);
            if (!folderManager.TryEnsureFolder())
                return;

            var data = Parse(inputFolder);

            WriteJsons(folderManager.OututFolder, data);

            Console.WriteLine("End");
        }

        private static void WriteJsons(string outputFolder, IReadOnlyCollection<DayDto> data)
        {
            var writters = new IJsonFileWritter[]
            {
                new DayJsonFileWritter(),
                new WeekJsonFileWritter()
            };

            foreach (var writter in writters)
                writter.Write(outputFolder, data);
        }

        private static IReadOnlyCollection<DayDto> Parse(string inputFolder)
        {
            var data = new List<DayDto>();
            var csvParser = new CsvParser();
            foreach (var csvPath in Directory.GetFiles(inputFolder, "*.csv"))
            {
                data = csvParser.Parse(data, csvPath);
            }

            return data;
        }

        private static bool TryGetInputFolder(string[] args, out string inputFolder)
        {
            inputFolder = string.Empty;
            var paramManager = new ParamManager();
            if (!paramManager.TryLoadParams(args))
                return false;

            inputFolder = paramManager.InputFolder;
            return true;
        }
    }
}
