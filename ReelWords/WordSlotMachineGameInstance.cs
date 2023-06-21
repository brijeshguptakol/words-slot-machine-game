using System;
using System.Collections.Generic;
using System.Linq;
using ReelWords.Models;

namespace ReelWords
{
    public class WordSlotMachineGameInstance
	{
        private readonly char[] AvailableLetters;
        private readonly IDictionary<char, IList<int>> LetterToIndexMap;
        private readonly IList<CharacterSelection> SelectedLetters;

		public WordSlotMachineGameInstance(char[] availableLetters)
		{
			AvailableLetters = availableLetters;
            SelectedLetters = new List<CharacterSelection>();
            LetterToIndexMap = CreateLetterToIndexMap();
        }

		public bool Process(char ch)
		{
            ch = char.ToLower(ch);

			// Check if the inout character is present in the available list of characters 
			if (LetterToIndexMap.ContainsKey(ch))
			{
				IList<int> allPositions = LetterToIndexMap[ch];

				// check if there is a positive value in the list of indices.
				// positive index signifies unused character.
				// create its negative equivalent to mark it occupied and preserve its index value
				for (int i = 0; i < allPositions.Count; i++)
				{
					if(allPositions[i] >= 0)
					{
						CharacterSelection selection = new CharacterSelection(allPositions[i], ch);
                        SelectedLetters.Add(selection);

                        allPositions[i] = -1;
                        return true;
                    }
                }

            }

			return false;
        }

        public int GetRemainingSlots()
        {
            return AvailableLetters.Length - SelectedLetters.Count;
        }

        public char[] GetSelectedReelCharactersWithIndex()
        {
            char[] selectedCharWithPreservedIndex = new char[AvailableLetters.Length];

            foreach (var selection in SelectedLetters)
            {
                selectedCharWithPreservedIndex[selection.Index] = selection.Character;
            }

            return selectedCharWithPreservedIndex;
        }

        public string GetSelectedWord()
        {
            char[] selectedWord = SelectedLetters.Select(x => x.Character).ToArray();
            return new string(selectedWord);
        }

        public void Display()
		{

            Console.WriteLine("+---++---++--AVAILABLE--++---++---+");

            foreach (char c in AvailableLetters)
            {
                Console.Write($"| {c} |");
            }
            Console.WriteLine();
            Console.WriteLine("+---++---++---++---++---++---++----+");
            Console.WriteLine();

            Console.WriteLine("+---++---++--SELECTED---++---++----+");

            foreach (CharacterSelection selection in SelectedLetters)
            {
                Console.Write($"| {selection.Character} |");
            }

            for (int i = 0; i < AvailableLetters.Length-SelectedLetters.Count; i++)
            {
                Console.Write($"|   |");
            }

            Console.WriteLine();
            Console.WriteLine("+---++---++---++---++---++---++----+");
            Console.WriteLine();
        }

        private IDictionary<char, IList<int>> CreateLetterToIndexMap()
        {
            IDictionary<char, IList<int>> map = new Dictionary<char, IList<int>>();

            for (int i = 0; i < AvailableLetters.Length; i++)
            {
                char ch = AvailableLetters[i];
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

    }
}

