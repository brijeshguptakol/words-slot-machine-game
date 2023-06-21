using System;
using System.IO;
using Moq;
using ReelWords.Logger;
using ReelWords.Utils.File;
using Xunit;

namespace ReelWordsTests.Utils.File
{
	public class SimpleFileReaderTests
    {
        readonly string RESOURCE_FOLDER_PATH = Path.Combine(Environment.CurrentDirectory, "Resources");
        private const string FileName = "data.txt";

        private Mock<ILogger> loggerMock;

        public SimpleFileReaderTests()
        {
            loggerMock = new Mock<ILogger>();
        }

        private SimpleFileReader CreateSimpleFileReader()
        {
            return new SimpleFileReader(RESOURCE_FOLDER_PATH, loggerMock.Object);
        }

        [Fact]
        public void ReadLinesFromFile_ValidFileName_ReturnsLines()
        {
            // Arrange
            var fileReader = CreateSimpleFileReader();
            var expectedLines = new string[] { "Line 1", "Line 2", "Line 3" };

            // Act
            var lines = fileReader.ReadLinesFromFile(FileName);

            // Assert
            Assert.Equal(expectedLines, lines);
        }

        [Fact]
        public void ReadLinesFromFile_InvalidFileName_ReturnsEmptyArray()
        {
            // Arrange
            var fileReader = CreateSimpleFileReader();
            var invalidFileName = "nonexistent.txt";

            // Act
            var lines = fileReader.ReadLinesFromFile(invalidFileName);

            // Assert
            Assert.Empty(lines);
        }
    }
}

