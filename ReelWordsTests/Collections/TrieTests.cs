using ReelWords.Repository;
using Xunit;

namespace ReelWordsTests.Collections
{
    public class TrieTests
    {
        private const string APPLES = "apples";
        private const string APPLE = "apple";
        private const string APP = "app";
        private const string VALID_WORD = "parallel";
        private const string PARA = "para";
        private const string WORD_WITH_NUMBER = "ABC123";
        private const string WORD_WITH_SPECIAL_CHAR = "ABC@";
        private const string NEW_WORD = "newword";


        [Theory]
        [InlineData(WORD_WITH_NUMBER, false)]
        [InlineData(WORD_WITH_SPECIAL_CHAR, false)]
        [InlineData(VALID_WORD, true)]
        [InlineData(NEW_WORD, true)]
        public void TrieInsertTest(string word, bool expected)
        {
            // Arrange
            Trie trie = new Trie();

            // Act
            bool actual = trie.Insert(word);

            // Assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(WORD_WITH_NUMBER, false)]
        [InlineData(WORD_WITH_SPECIAL_CHAR, false)]
        [InlineData(VALID_WORD, true)]
        [InlineData(NEW_WORD, false)]
        [InlineData(PARA, false)]
        public void TrieSearchTest(string word, bool expected)
        {
            // Arrange
            Trie trie = new Trie();
            bool inserted = trie.Insert(VALID_WORD);
            Assert.True(inserted);

            // Act
            bool actual = trie.Search(word);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(APP)]
        [InlineData(APPLE)]
        public void TrieDeleteTest(string word)
        {
            // Arrange
            Trie trie = new Trie();
            trie.Insert(word);
            Assert.True(trie.Search(word));

            // Act
            trie.Delete(word);

            // Assert
            Assert.False(trie.Search(word));
        }

        [Fact]
        public void TrieDelete_WithInvalidWord_DoesNotModifyTrie()
        {
   
            // Arrange
            Trie trie = new Trie();
            trie.Insert(APPLE);

            // Act
            trie.Delete(APP);

            // Assert
            Assert.True(trie.Search(APPLE));
        }

        [Fact]
        public void TrieDelete_WithValidWord_DoesNotAffectsOtherWordsInSamePath()
        {
            // Arrange
            Trie trie = new Trie();
            trie.Insert(APPLES);
            trie.Insert(APPLE);
            trie.Insert(APP);


            // Act
            Assert.True(trie.Search(APP));
            trie.Delete(APP);

            // Assert
            Assert.True(trie.Search(APPLES));
            Assert.True(trie.Search(APPLE));
            Assert.False(trie.Search(APP));


            // Act
            trie.Delete(APPLES);

            // Assert
            Assert.False(trie.Search(APPLES));
            Assert.True(trie.Search(APPLE));
            Assert.False(trie.Search(APP));


        }

    }
}