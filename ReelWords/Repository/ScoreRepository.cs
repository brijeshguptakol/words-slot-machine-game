using System.Collections.Generic;
using ReelWords.Logger;
using ReelWords.Repository.Interface;
using ReelWords.Utils.File;

namespace ReelWords.Repository
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly string SCORE_FILE_NAME = "scores.txt";

        private readonly IFileReader FileReader;
        private readonly ILogger Logger;

        private readonly IDictionary<char, int> ScoreRatingTable;

        public ScoreRepository(ILogger logger, IFileReader fileReader)
        {
            FileReader = fileReader;
            Logger = logger;

            ScoreRatingTable = new Dictionary<char, int>();

            LoadFromFile();
        }

        public int GetWeightByAlphabet(char ch)
        {
            if (ScoreRatingTable.ContainsKey(ch))
            {
                return ScoreRatingTable[ch];
            }

            return 0;
        }

        #region Private Methhods

        private void LoadFromFile()
        {
            string[] lines = FileReader.ReadLinesFromFile(SCORE_FILE_NAME);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] lineParts = line.Split(' ');
                if (lineParts.Length == 2)
                {
                    char alphabet = lineParts[0][0];
                    int weight = TryParseToIntegerOrDefault(lineParts[1]);
                    ScoreRatingTable[alphabet] = weight;

                }
                else
                {
                    Logger.Error($"Could not parse line - {i} with value - {line}");
                }

            }
        }

        private int TryParseToIntegerOrDefault(string weight)
        {

            if (!int.TryParse(weight, out int value))
            {
                Logger.Error($"{weight} is not a valid integer");
            }

            return value;

        }

        #endregion

    }
}

