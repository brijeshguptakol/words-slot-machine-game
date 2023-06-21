using ReelWords.Logger;
using ReelWords.Repository.Interface;
using ReelWords.Services.Interface;

namespace ReelWords.Services
{
	public class ScoreService : IScoreService
    {
        private readonly ILogger Logger;
        private readonly IScoreRepository ScoreRepository;


        public ScoreService(ILogger logger, IScoreRepository scoreRepository)
        {
            Logger = logger;
            ScoreRepository = scoreRepository;
        }

        public int CalculateScore(string word)
        {
            int totalScore = 0;

            foreach (char ch in word)
            {
                totalScore += ScoreRepository.GetWeightByAlphabet(ch);
            }

            return totalScore;
        }
    }
}

