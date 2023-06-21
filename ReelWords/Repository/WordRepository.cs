using ReelWords.Logger;
using ReelWords.Repository.Interface;
using ReelWords.Utils.File;

namespace ReelWords.Repository
{
    public class WordRepository : IWordRepository
    {
        private readonly string ENGLISH_WORDS_FILE_NAME = "american-english-large.txt";

        private readonly Trie WordStore;

        private readonly IFileReader FileReader;
        private readonly ILogger Logger;

        public WordRepository(ILogger logger, IFileReader fileReader)
        {
            Logger = logger;
            FileReader = fileReader;

            WordStore = new Trie();
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            string[] words = FileReader.ReadLinesFromFile(ENGLISH_WORDS_FILE_NAME);
            foreach (string word in words)
            {
                Insert(word);
            }
        }

        private bool Insert(string word)
        {
            return WordStore.Insert(word);
        }

        public void Delete(string word)
        {
            WordStore.Delete(word);
        }

        public bool Search(string word)
        {
            return WordStore.Search(word);
        }

    }

}

