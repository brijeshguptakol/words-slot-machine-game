using ReelWords.Logger;
using ReelWords.Repository.Interface;
using ReelWords.Utils.Services;

namespace ReelWords.Services
{
	public class ReelService : IReelService
	{
        private readonly ILogger Logger;
        private readonly IReelRepository ReelRepository;

        public ReelService(ILogger logger, IReelRepository reelRepository)
        {
            Logger = logger;
            ReelRepository = reelRepository;
        }

        /// <summary>
        /// Returns the Reel (array of alphabets) 
        /// </summary>
        /// <returns></returns>
        public char[] GetCurrentReel()
        {
            return ReelRepository.GetCurrentReel();
        }

        public void Shuffle(char[] playedWord)
        {
            ReelRepository.Shuffle(playedWord);
        }
    }
}

