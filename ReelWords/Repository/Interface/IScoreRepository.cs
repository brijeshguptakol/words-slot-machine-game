using System.Collections.Generic;

namespace ReelWords.Repository.Interface
{
    public interface IReelRepository
    {

        /// <summary>
        /// Get the next list of characters to play.
        /// </summary>
        /// <returns></returns>
        IDictionary<char, IList<int>> GetReel();

        /// <summary>
        /// Get the next list of characters to play.
        /// </summary>
        /// <returns></returns>
        char[] GetCurrentReel();

        /// <summary>
        /// Shuffle the Reels based on the word played.
        /// </summary>
        /// <param name="playedWord"></param>
        void Shuffle(char[] playedWord);

        void PrintCurrentState();

    }
}

