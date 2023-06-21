namespace ReelWords.Services.Interface
{
    public interface IScoreService
    {
        /// <summary>
        /// Calculate the score for the input word
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        int CalculateScore(string word);
    }
}

