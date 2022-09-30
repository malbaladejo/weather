using System;
using System.IO;

namespace WeatherImport
{
    internal class FolderManager
    {
        public FolderManager(string inputFolder)
        {
            this.InputFolder = inputFolder;
            this.OututFolder = Path.Combine(this.InputFolder, "output");
            this.ArchiveFolder = Path.Combine(this.InputFolder, "archive");
        }

        public bool TryEnsureFolder()
        {
            Console.WriteLine($"Import folder {this.InputFolder}.");

            if (!this.TryEnsureFolder(this.OututFolder))
                return false;

            return this.TryEnsureFolder(this.ArchiveFolder);
        }

        private bool TryEnsureFolder(string folder)
        {
            try
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fail to create {folder}.");
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public string InputFolder { get; }

        public string OututFolder { get; }

        public string ArchiveFolder { get; }
    }
}
