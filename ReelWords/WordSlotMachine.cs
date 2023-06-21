using System;
using ReelWords.Logger;
using ReelWords.Services.Interface;
using ReelWords.Utils.Services;

namespace ReelWords
{
    public class WordSlotMachineGame 
    {
		private readonly IReelService ReelService;
        private readonly IWordService WordService;
        private readonly IScoreService ScoreService;
        private readonly ILogger Logger;

        public WordSlotMachineGame(ILogger logger, IReelService reelService, IWordService wordService, IScoreService scoreService)
		{
			Logger = logger;
            ReelService = reelService;
            WordService = wordService;
            ScoreService = scoreService;
        }

        public void Start()
        {
            char[] reel = ReelService.GetCurrentReel();
            WordSlotMachineGameInstance instance = new WordSlotMachineGameInstance(reel);

            bool isPlaying = true;

            while (isPlaying)
            {
                DisplayCurrentState(instance);

                if (instance.GetRemainingSlots() == 0)
                {
                    Submit(instance);
                    break;
                }

                Console.Write("Type: ");
                ConsoleKeyInfo input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.Enter:
                        Submit(instance);
                        isPlaying = false;
                        break;
                    case ConsoleKey.Escape:
                        Exit(instance);
                        isPlaying = false;
                        break;
                    default:
                        instance.Process(input.KeyChar);
                        break;
                }
                
            }

        }

        public void Submit(WordSlotMachineGameInstance instance)
        {
            int score = CalculateScore(instance);
            DisplayScore(score);

            UpdateReel(instance);
        }

        public void Exit(WordSlotMachineGameInstance instance)
        {
            Console.WriteLine("Game was terminated by the user.");
        }

        public static void DisplayCurrentState(WordSlotMachineGameInstance instance)
        {
            Console.Clear();

            Console.WriteLine("+---++---WORD SLOT MACHINE---++----+");
            Console.WriteLine();
            Console.WriteLine("+---++--------WELCOME--------++----+");
            Console.WriteLine();
            Console.WriteLine("Press Enter to Submit or ESC to exit the game.");
            Console.WriteLine("Use the Available alphabets to form a word.");

            Console.WriteLine();

            instance.Display();
        }

        #region Private Methods

        private int CalculateScore(WordSlotMachineGameInstance instance)
        {
            string selectedWord = instance.GetSelectedWord();

            // Check if selected word is a Valid word in Dictionary
            if (!WordService.Search(selectedWord)) return 0;

            return ScoreService.CalculateScore(selectedWord);
        }

        private static void DisplayScore(int score)
        {
            Console.WriteLine($"YOUR SCORE - {score}");
        }

        private void UpdateReel(WordSlotMachineGameInstance instance)
        {
            char[] selectedCharWithPreservedIndex = instance.GetSelectedReelCharactersWithIndex();
            ReelService.Shuffle(selectedCharWithPreservedIndex);
        }

        #endregion
    }
}

