namespace ReelWords.Repository.Interface
{
    public interface IScoreRepository
    {
        int GetWeightByAlphabet(char ch);
    }
}

