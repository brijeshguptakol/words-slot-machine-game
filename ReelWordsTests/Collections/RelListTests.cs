
using ReelWords.Logger;
using ReelWords.Repository;
using Xunit;
using Moq;

namespace ReelWordsTests.Collections
{
	public class ReelListTests
	{

        private Mock<ILogger> loggerMock;

        public ReelListTests()
        {
            loggerMock = new Mock<ILogger>();
        }

        private ReelList CreateReelList()
        {
            return new ReelList(loggerMock.Object);
        }

        [Fact]
        public void AddReels_WithValidReels_AddsReelsToList()
        {
            // Arrange
            var reelList = CreateReelList();
            var lines = new string[]
            {
            "u d x c l a e",
            "e y v p q y n",
            "i l o w m g n",
            "a n d i s e v",
            "a n j a e t b",
            "a b w t d o h"
            };

            // Act
            reelList.AddReels(lines);

            // Assert
            for (int i=1; i<= lines.Length; i++)
            {
                string lastLine = lines[lines.Length - i];
                Assert.Equal(lastLine.Replace(" ", "").ToCharArray(), reelList.CurrentReel);
                reelList.ShuffleReel(reelList.CurrentReel);
            }
        }

        [Fact]
        public void CurrentReel_EmptyReelList_ReturnsNull()
        {
            // Arrange
            var reelList = CreateReelList();

            // Act
            var currentReel = reelList.CurrentReel;

            // Assert
            Assert.Null(currentReel);
        }

        [Fact]
        public void CurrentReel_FilledReelList_ReturnsCurrentReel()
        {
            // Arrange
            var reelList = CreateReelList();
            var reels = new string[]
            {
            "u d x c l a e",
            "e y v p q y n",
            "i l o w m g n",
            "a n d i s e v",
            "a n j a e t b",
            "a b w t d o h"
            };
            reelList.AddReels(reels);

            // Act
            var currentReel = reelList.CurrentReel;

            // Assert
            Assert.NotNull(currentReel);
            Assert.Equal(reels[reels.Length - 1].Replace(" ", "").ToCharArray(), currentReel);
        }

        [Fact]
        public void ShuffleReel_MatchingCharacters_ShiftsCharactersToTop()
        {
            // Arrange
            var reelList = CreateReelList();
            var reels = new string[]
            {
            "u d x c l a e",
            "e y v p q y n",
            "i l o w m g n",
            "a n d i s e v",
            "a n j a e t b",
            "a b w t d o h"
            };

            reelList.AddReels(reels);
            char[] playedWordChars = new char[7];
            playedWordChars[1] = 'b';
            playedWordChars[0] = 'a';
            playedWordChars[3] = 't';

            // Act
            reelList.ShuffleReel(playedWordChars);

            // Assert
            char[] expectedNextReel = new char[] { 'a', 'n', 'w', 'a', 'd', 'o', 'h' };

            Assert.Equal(expectedNextReel, reelList.CurrentReel);
        }

    }
}

