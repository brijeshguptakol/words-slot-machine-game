using System.Collections.Generic;
using ReelWords.Logger;
using ReelWords.Repository.Interface;
using ReelWords.Utils.File;

namespace ReelWords.Repository
{
    public class ReelRepository : IReelRepository
    {
        private readonly string REELS_FILE_NAME = "reels.txt";

        private readonly ReelList Reels;

        private readonly ILogger Logger;
        private readonly IFileReader FileReader;


        public ReelRepository(ILogger logger, IFileReader fileReader)
        {
            Logger = logger;
            FileReader = fileReader;
            Reels = new ReelList(logger);
            LoaaFromFile();
        }

        public IDictionary<char, IList<int>> GetReel()
        {
            IDictionary<char, IList<int>> map = new Dictionary<char, IList<int>>();

            for (int i = 0; i < Reels.CurrentReel.Length; i++)
            {
                char ch = Reels.CurrentReel[i];
                if (map.ContainsKey(ch))
                {
                    map[ch].Add(i);
                }
                else
                {
                    map[ch] = new List<int> { i };
                }
            }

            return map;
        }

        public char[] GetCurrentReel()
        {
            return Reels.CurrentReel;
        }

        public void Shuffle(char[] playedWord)
        {
            Reels.ShuffleReel(playedWord);
        }

        public void PrintCurrentState()
        {
            Reels.Print();
        }

        #region Private Methods

        private void LoaaFromFile()
        {
            string[] lines = FileReader.ReadLinesFromFile(REELS_FILE_NAME);

            Reels.AddReels(lines);
        }

        #endregion

    }
}

