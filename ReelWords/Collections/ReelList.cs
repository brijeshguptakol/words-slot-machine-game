using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ReelWords.Logger;

namespace ReelWords.Repository
{
    public class ReelList
    {
        private const string VALID_WORD_REGEX = @"^[a-z]+$";
        private const int REEL_LENGTH = 7;

        private readonly ILogger logger;

        private readonly IDictionary<int, ReelNode> list;

        private char[] _currentReel;
        private int _height;
         
        public ReelList(ILogger logger)
        {
            this.logger = logger;
            list = new Dictionary<int, ReelNode>();

            for (int i = 0; i < REEL_LENGTH; i++)
            {
                list[i] = new ReelNode();
            }
            
        }

        #region Add Reels

        /// <summary>
        /// create the ReelList using the array of lines.
        /// Each character is seperated by space.
        /// </summary>
        /// <param name="lines"></param>
        public void AddReels(string[] lines)
        {
            foreach (string line in lines)
            {
                char[] charArray = line.Replace(" ", "").ToCharArray();
                if (IsValid(charArray))
                {
                    FillEachReelInSequence(charArray);
                }

                _height++;
            }
        }

        private static bool IsValid(char[] reel)
        {
            if (reel.Length != REEL_LENGTH)
            {

                return false;
            }
            string reelString = new string(reel);
            return Regex.IsMatch(reelString, VALID_WORD_REGEX);
        }

        private void FillEachReelInSequence(char[] chars)
        {
            for (int i = 0; i < REEL_LENGTH; i++)
            {
                ReelNode reel = list[i];
                char ch = chars[i];
                reel.Add(ch);
            }
        }

        #endregion

        /// <summary>
        /// Fetch the Current word to play.
        /// Current is formed by last characters of each ReelNode in ReelList
        /// </summary>
        /// <returns></returns>
        ///
        public char[] CurrentReel
        {
            get
            {
                if (_currentReel != null) return _currentReel;

                try
                {
                    char[] word = new char[REEL_LENGTH];

                    for (int i = 0; i < REEL_LENGTH; i++)
                    {
                        word[i] = list[i].GetLastChar();
                    }

                    return _currentReel = word;
                }
                catch (Exception ex)
                {
                    logger.Error("Failed to Fetch Current Word", ex);
                    return  null;
                }

            }
        }

        /// <summary>
        /// This method moves the played character to the top of the column.
        /// The next character in the list is available for the enxt game.
        /// </summary>
        /// <param name="playedWordChars"></param>
        public void ShuffleReel(char[] playedWordChars)
        {
            for (int i = 0; i < CurrentReel.Length; i++)
            {
                char currentChar = CurrentReel[i];
                char playedChar = playedWordChars[i];

                //If current char was part of the played word, then shift the corresponding reel char
                if(playedChar == currentChar)
                {
                    ReelNode node = list[i];
                    node.MoveCurrentToTop();
                }
            }

            //Reset _currentWord to null
            _currentReel = null;
        }

        #region Debug

        /// <summary>
        /// For debug purpose only
        /// </summary>
        public void Print()
        {
            try
            {
                char[] word = new char[REEL_LENGTH];

                for (int h = 1; h <= _height; h++)
                {
                    for (int i = 0; i < REEL_LENGTH; i++)
                    {
                        ReelNode node = list[i];
                        Console.Write(node.GetFirstChar() + " ");
                        node.MoveCurrentToBottom();
                    }

                    Console.WriteLine();
                }

            }
            catch (Exception ex)
            {
                logger.Error("Failed to Print", ex);
            }
        }

        #endregion
    }

    internal class ReelNode
	{
        private readonly LinkedList<char> reel;

        public ReelNode()
        {
            reel = new LinkedList<char>();
        }

        public void Add(char ch)
        {
            reel.AddLast(ch);
        }

        public char GetLastChar()
        {
            if (reel.Last != null) return reel.Last.Value;

            throw new Exception("Reel is empty");
        }

        internal void MoveCurrentToTop()
        {
            if (reel.Last != null && reel.First != reel.Last)
            {
                LinkedListNode<char> last = reel.Last;
                reel.RemoveLast();
                reel.AddFirst(last);
            }
        }

        #region Debug

        /// <summary>
        /// For debug purpose only
        /// </summary>
        internal char GetFirstChar()
        {
            if (reel.First != null) return reel.First.Value;

            throw new Exception("Reel is empty");
        }

        /// <summary>
        /// For debug purpose only
        /// </summary>
        internal void MoveCurrentToBottom()
        {
            if (reel.First != null && reel.First != reel.Last)
            {
                LinkedListNode<char> first = reel.First;
                reel.RemoveFirst();
                reel.AddLast(first);
            }
        }

        #endregion
    }
}

