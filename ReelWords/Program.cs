using System;
using ReelWords.Logger;
using ReelWords.Utils.File;
using ReelWords.Services.Interface;
using ReelWords.Services;
using System.IO;
using ReelWords.Repository.Interface;
using ReelWords.Repository;
using ReelWords.Utils.Services;

namespace ReelWords
{
    public static class Program
    {
        static void Main(string[] args)
        {
            WordSlotMachineGame game = CreateGame();

            bool playing = true;

            while (playing)
            {
                game.Start();

                Console.WriteLine();
                Console.WriteLine("Press ESC to Exit or any other key to continue.");


                ConsoleKeyInfo input = Console.ReadKey();

                if (input.Key == ConsoleKey.Escape)
                {
                    playing = false;
                }

                Console.Clear();
            }
        }

        private static WordSlotMachineGame CreateGame()
        {
            //TODO Read From environment variable
            string RESOURCE_FOLDER_PATH = Path.Combine(Environment.CurrentDirectory, "Resources");

            //Logger
            ILogger logger = ConsoleLogger.Instance;

            //Utils
            IFileReader fileReader = new SimpleFileReader(RESOURCE_FOLDER_PATH, logger);

            //Repositories
            IScoreRepository scoreRepository = new ScoreRepository(logger, fileReader);
            IReelRepository reelRepository = new ReelRepository(logger, fileReader);
            IWordRepository wordRepository = new WordRepository(logger, fileReader);

            // Services
            IScoreService scoreService = new ScoreService(logger, scoreRepository);
            IReelService reelService = new ReelService(logger, reelRepository);
            IWordService wordService = new WordService(logger, wordRepository);

            return new WordSlotMachineGame(logger, reelService, wordService, scoreService);
        }

    }


    
}