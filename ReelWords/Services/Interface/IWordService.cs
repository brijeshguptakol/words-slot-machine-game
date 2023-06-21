namespace ReelWords.Services.Interface
{
    public interface IWordService
    {
        /// <summary>
        /// Delete input word from the word store
        /// </summary>
        /// <param name="word"></param>
        void Delete(string word);

        /// <summary>
        /// Search if the input word is present in the word store.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        bool Search(string word);
    }
}

