using System;
using System.IO;
using System.Reflection;

namespace WeatherImport
{
    internal class ParamManager
    {
        public string InputFolder { get; private set; }

        public bool TryLoadParams(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Running in script folder.");
                this.InputFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                return true;
            }

            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine($"the folder \"{args[0]}\" does not exists.");
                this.InputFolder = "";
                return false;
            }

            this.InputFolder = args[0];
            return true;
        }
    }
}
