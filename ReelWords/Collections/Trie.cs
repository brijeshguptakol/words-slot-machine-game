using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ReelWords.Repository
{
    /*public class Trie
    {
        public bool Search(string s)
        {
            throw new NotImplementedException();
        }

        public void Insert(string s)
        {
            throw new NotImplementedException();
        }

        public void Delete(string s)
        {
            throw new NotImplementedException();
        }
    }
    */

    public class Trie
    {
        private readonly TrieNode _root;

        //Contructor 
        public Trie()
        {
            _root = new TrieNode();
        }

        public bool Search(string s)
        {
            return _root.Search(s);
        }

        public bool Insert(string s)
        {
            return _root.Insert(s);
        }

        public void Delete(string s)
        {
            _root.Delete(s);
        }

    }

    internal class TrieNode
    {
        private const string VALID_WORD_REGEX = @"^[a-z]+$";
        private readonly Dictionary<char, TrieNode> _children;
        private bool _isEndOfWord;

        public TrieNode()
        {
            _children = new Dictionary<char, TrieNode>();
            _isEndOfWord = false;
        }

        public bool Insert(string word)
        {
            if (!IsValidWord(word))
            {
                return false;
            }

            TrieNode current = this;

            foreach (char c in word)
            {
                if (!current._children.ContainsKey(c))
                    current._children[c] = new TrieNode();

                current = current._children[c];
            }

            current._isEndOfWord = true;

            return true;
        }

        public bool Search(string word)
        {
            if (!IsValidWord(word))
                return false;

            TrieNode current = this;

            foreach (char c in word)
            {
                if (!current._children.ContainsKey(c))
                    return false;

                current = current._children[c];
            }

            return current._isEndOfWord;
        }

        public void Delete(string word)
        {
            if (!IsValidWord(word))
                return;

            DeleteInternal(this, word, 0);
        }

        /// <summary>
        /// Recurrersively visits the last character of the given word and marks the IsEndOfWord to be false.
        /// Also deletes the characters in the path which are not part of any other word.
        /// Return value of this method does not implies success of the Delete operation.
        /// Its is just used in recurrsion to decide the deletion of the childNode
        /// </summary>
        /// <param name="node"></param>
        /// <param name="word"></param>
        /// <param name="index"></param>
        /// <returns>
        /// Return value of this method does not implies success of the Delete operation.
        /// Its is just used in recurrsion to decide the deletion of the childNode
        /// </returns>
        private bool DeleteInternal(TrieNode node, string word, int index)
        {
            if (index == word.Length)
            {
                if (!node._isEndOfWord)
                    return false;

                node._isEndOfWord = false;

                return node._children.Count == 0; // Check if the node has no children
            }

            char currentChar = word[index];

            if (!node._children.ContainsKey(currentChar))
                return false;

            TrieNode childNode = node._children[currentChar];
            bool shouldDeleteChild = DeleteInternal(childNode, word, index + 1);

            if (shouldDeleteChild)
            {
                node._children.Remove(currentChar);
                return node._children.Count == 0 && !node._isEndOfWord; // Check if the node has no children and is not the end of a word
            }

            return false;
        }

        private static bool IsValidWord(string word)
        {
            return Regex.IsMatch(word, VALID_WORD_REGEX);
        }
    }
}
