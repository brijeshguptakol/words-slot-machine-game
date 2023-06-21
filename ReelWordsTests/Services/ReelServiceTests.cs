using Moq;
using ReelWords.Logger;
using ReelWords.Repository.Interface;
using ReelWords.Services;
using Xunit;

namespace ReelWordsTests.Services
{
	public class ReelServiceTests
	{
        private Mock<ILogger> loggerMock;
        private Mock<IReelRepository> reelRepositoryMock;

        public ReelServiceTests()
        {
            loggerMock = new Mock<ILogger>();
            reelRepositoryMock = new Mock<IReelRepository>();
        }

        private ReelService CreateReelService()
        {
            return new ReelService(loggerMock.Object, reelRepositoryMock.Object);
        }

        [Fact]
        public void GetCurrentReel_InvokesReelRepositoryGetCurrentReel()
        {
            // Arrange
            var reelService = CreateReelService();
            var expectedReel = new char[] { 'u', 'd', 'x', 'c', 'l', 'a', 'e' };
            reelRepositoryMock.Setup(repo => repo.GetCurrentReel()).Returns(expectedReel);

            // Act
            var currentReel = reelService.GetCurrentReel();

            // Assert
            reelRepositoryMock.Verify(repo => repo.GetCurrentReel(), Times.Once);
            Assert.Equal(expectedReel, currentReel);
        }

        [Fact]
        public void Shuffle_InvokesReelRepositoryShuffle()
        {
            // Arrange
            var reelService = CreateReelService();
            var playedWord = new char[] { 'l', 'a', 'c', 'e' };

            // Act
            reelService.Shuffle(playedWord);

            // Assert
            reelRepositoryMock.Verify(repo => repo.Shuffle(playedWord), Times.Once);
        }
    }
}

