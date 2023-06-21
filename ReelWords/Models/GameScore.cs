namespace ReelWords.Models
{
	public class GameScore
	{
		public string word { get; private set; }
		public int score { get; private set; }

		public GameScore(string word, int score)
		{
			this.word = word;
			this.score = score;
		}
	}
}

