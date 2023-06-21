using ReelWords.Logger;
using ReelWords.Repository.Interface;
using ReelWords.Services.Interface;

namespace ReelWords.Services
{
	public class WordService : IWordService
	{
        private readonly ILogger Logger;
        private readonly IWordRepository WordRepository;

        public WordService(ILogger logger, IWordRepository wordRepository)
        {
            Logger = logger;
            WordRepository = wordRepository;
        }

        public void Delete(string word)
        {
            WordRepository.Delete(word);
        }

        public bool Search(string word)
        {
            return WordRepository.Search(word);
        }
    }
}

