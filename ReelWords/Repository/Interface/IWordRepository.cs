namespace ReelWords.Repository.Interface
{
    public interface IWordRepository
    {
        void Delete(string word);

        bool Search(string word);
    }
}

