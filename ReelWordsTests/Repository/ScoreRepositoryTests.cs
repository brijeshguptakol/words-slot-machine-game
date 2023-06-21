using Moq;
using ReelWords.Repository;
using ReelWords.Utils.File;
using Xunit;
using ReelWords.Logger;

namespace ReelWordsTests.Repository
{
    public class ScoreRepositoryTests
    {
        private const string ScoreFileName = "scores.txt";

        private Mock<IFileReader> fileReaderMock;
        private Mock<ILogger> loggerMock;

        public ScoreRepositoryTests()
        {
            fileReaderMock = new Mock<IFileReader>();
            loggerMock = new Mock<ILogger>();
        }

        private ScoreRepository CreateScoreRepository()
        {
            return new ScoreRepository(loggerMock.Object, fileReaderMock.Object);
        }

        [Fact]
        public void GetWeightByAlphabet_ExistingAlphabet_ReturnsWeight()
        {
            // Arrange
            fileReaderMock.Setup(fr => fr.ReadLinesFromFile(ScoreFileName)).Returns(new string[]
            {
                "a 1",
                "b 2",
                "c 3"
            });
            var scoreRepository = CreateScoreRepository();

            // Act and Assert
            Assert.Equal(1, scoreRepository.GetWeightByAlphabet('a'));
            Assert.Equal(2, scoreRepository.GetWeightByAlphabet('b'));
            Assert.Equal(3, scoreRepository.GetWeightByAlphabet('c'));
        }

        [Fact]
        public void GetWeightByAlphabet_NonExistingAlphabet_ReturnsZero()
        {
            // Arrange
            fileReaderMock.Setup(fr => fr.ReadLinesFromFile(ScoreFileName)).Returns(new string[]
            {
                "a 1",
                "b 2",
                "c 3"
            });
            var scoreRepository = CreateScoreRepository();

            // Act
            var weight = scoreRepository.GetWeightByAlphabet('d');

            // Assert
            Assert.Equal(0, weight);
        }



        [Fact]
        public void LoadFromFile_InvalidLine_LogsError()
        {
            // Arrange

            var lines = new[]
            {
                "a 5",
                "b",
                "c 15"
            };
            fileReaderMock.Setup(fr => fr.ReadLinesFromFile(ScoreFileName)).Returns(lines);



            // Act
            var scoreRepository = CreateScoreRepository();

            // Assert
            loggerMock.Verify(logger => logger.Error("Could not parse line - 1 with value - b"), Times.Once);
        }
       
        [Fact]
        public void TryParseToIntegerOrDefault_InvalidInteger_LogsErrorAndReturnsZero()
        {
            // Arrange
            var lines = new[]
            {
                "a $",
                "b 2",
                "c 15"
            };
            fileReaderMock.Setup(fr => fr.ReadLinesFromFile(ScoreFileName)).Returns(lines);
           
            // Act
            var scoreRepository = CreateScoreRepository();

            // Assert
            loggerMock.Verify(logger => logger.Error("$ is not a valid integer"), Times.Once);
        }
    }
}

