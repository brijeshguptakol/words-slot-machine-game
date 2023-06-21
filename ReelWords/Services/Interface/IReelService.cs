namespace ReelWords.Utils.Services
{
    public interface IReelService
    {
        /// <summary>
        /// Get the current reel, i.e alphabets to play the word slot machine game
        /// </summary>
        /// <returns></returns>
        char[] GetCurrentReel();

        /// <summary>
        /// Shuffle the ReelList based on the selected word by the user.
        /// </summary>
        /// <param name="playedWord"></param>
        void Shuffle(char[] playedWord);
    }

}

