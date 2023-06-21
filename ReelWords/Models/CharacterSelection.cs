namespace ReelWords.Models
{
    public class CharacterSelection
    {
        public int Index { get; private set; }
        public char Character { get; set; }

        public CharacterSelection(int index, char ch)
        {
            Index = index;
            Character = ch;
        }
    }
}

