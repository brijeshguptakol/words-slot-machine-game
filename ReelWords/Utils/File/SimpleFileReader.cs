using System;
using System.IO;
using ReelWords.Logger;

namespace ReelWords.Utils.File
{
	public class SimpleFileReader: IFileReader
	{
        private readonly string ResourceFolderPath;
        private readonly ILogger logger;

        public SimpleFileReader(string resourceFolderPath, ILogger logger)
        {
            this.logger = logger;
            ResourceFolderPath = resourceFolderPath;
        }

        public string[] ReadLinesFromFile(string fileName)
        {
            string filePath = Path.Combine(ResourceFolderPath, fileName);
            try
            {
                return System.IO.File.ReadAllLines(filePath);
            }
            catch (Exception ex)
            {
                logger.Error($"Error reading file: {filePath}", ex);
                return Array.Empty<string>();
            }
        }
    }
}

